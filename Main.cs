
using System;
using System.IO;
using System.Collections.Generic;
using GardensPoint;

public class Compiler
{

    public static int errors = 0;

    public static List<string> source;

    public static List<string> labels = new List<string>();

    private static readonly int returnLabel = 0;

    public static SyntaxTree root;

    public static int lineno = 1;

    public enum TypeT { IntT, DoubleT, BoolT, VoidT }

    public static Dictionary<string, TypeT> declaredVariables = new Dictionary<string, TypeT>();


    // arg[0] określa plik źródłowy
    // pozostałe argumenty są ignorowane
    public static int Main(string[] args)
    {
        string file;
        FileStream source;
        Console.WriteLine("\nSingle-Pass CIL Code Generator for Multiline Calculator - Gardens Point");
        if (args.Length >= 1)
            file = args[0];
        else
        {
            Console.Write("\nsource file:  ");
            file = Console.ReadLine();
        }
        
        try
        {
            var sr = new StreamReader(file);
            string str = sr.ReadToEnd();
            sr.Close();
            Compiler.source = new System.Collections.Generic.List<string>(str.Split(new string[] { "\r\n" }, System.StringSplitOptions.None));
            source = new FileStream(file, FileMode.Open);
        }
        catch (Exception e)
        {          
            Console.WriteLine("\n" + e.Message);
            return 1;
        }
        Scanner scanner = new Scanner(source);
        Parser parser = new Parser(scanner);
        Console.WriteLine();
        bool isParsed = false;        
        try
        {
            isParsed = parser.Parse();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        source.Close();
        bool success = errors == 0 && isParsed;
        if (success)
        {
            sw = new StreamWriter(file + ".il");
            GenerateCode();
            sw.Close();
        }        
        if (success)
        {
            Console.WriteLine("  compilation successful\n");
        }
        else
        {
            Console.WriteLine($"\n  {errors} errors detected\n");
            File.Delete(file + ".il");
        }

        return success ? 0 : 2;
    }

    public static void EmitCode(string instr = null)
    {
        sw.WriteLine(instr);
    }

    public static void EmitCode(string instr, params object[] args)
    {
        sw.WriteLine(instr, args);
    }

    private static StreamWriter sw;

    private static void GenerateCode()
    {
        GenProlog();

        Compiler.EmitCode(".maxstack {0}", 128);
        EmitCode(".locals init (float64 temp)");
        foreach (var variable in declaredVariables)
        {
            EmitCode($".locals init ({TypeToStr(variable.Value)} {variable.Key})");
        }
        EmitCode();

        root.GenCode();
        EmitCode();

        EmitCode($"IL_{returnLabel}: nop");
        
        GenEpilog();
    }
    private static void GenProlog()
    {
        EmitCode(".assembly extern mscorlib { }");
        EmitCode(".assembly calculator { }");
        EmitCode(".method static void main()");
        EmitCode("{");
        EmitCode(".entrypoint");
        EmitCode(".try");
        EmitCode("{");
        EmitCode();

        EmitCode("// prolog");
        EmitCode();
    }

    private static void GenEpilog()
    {
        EmitCode("leave EndMain");
        EmitCode("}");
        EmitCode("catch [mscorlib]System.Exception");
        EmitCode("{");
        EmitCode("callvirt instance string [mscorlib]System.Exception::get_Message()");
        EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
        EmitCode("leave EndMain");
        EmitCode("}");
        EmitCode("EndMain: ret");
        EmitCode("}");
    }

    public static void SetError(string msg, int lineNumber)
    {
        Console.WriteLine("  line {0,3}:" + msg, lineNumber);
        ++errors;
    }


    public static void DeclareNewVariable(string id, Tokens token, int ln)
    {
        string name = string.Concat("_", id);
        if(declaredVariables.ContainsKey(name))
        {
            SetError("Variable already declared", ln);
            return;
        }
        TypeT type = token == Tokens.Bool ? TypeT.BoolT : (token == Tokens.Int ? TypeT.IntT : TypeT.DoubleT);
        declaredVariables.Add(name, type);
    }

    public static string GetNewLabel()
    {
        int nr = labels.Count + 1;
        string label =  $"IL_{nr}";
        labels.Add(label);
        return label;
    }
    public static string TypeToStr(TypeT type)
    {
        switch(type)
        {
            case TypeT.IntT:
                return "int32";
            case TypeT.DoubleT:
                return "float64";
            case TypeT.BoolT:
                return "bool";
            default:
                return "string";
        }
    }

    public abstract class SyntaxTree
    {
        public TypeT type;

        public int line = -1;
        public abstract TypeT CheckType();
        public abstract void GenCode();

    }
    public static void SetTypesOnStack(TypeT type1, TypeT type2)
    {
        TypeT type = (type1 == TypeT.IntT && type2 == TypeT.IntT) ? TypeT.IntT : TypeT.DoubleT;
        if (type1 != type)
        {
            Compiler.EmitCode("stloc temp");
            Compiler.EmitCode("conv.r8");
            Compiler.EmitCode("ldloc temp");
        }
        if (type2 != type)
            Compiler.EmitCode("conv.r8");
    }

    public class UnaryOp : SyntaxTree
    {
        private SyntaxTree expression;
        private Tokens kind;
        public UnaryOp(SyntaxTree e, Tokens k, int ln)
        {
            expression = e;
            kind = k;
            line = ln;

            expression.CheckType();
            ValidateTypes(expression.type, k);
        }

        private void ValidateTypes(TypeT argType, Tokens token)
        {
            switch (token)
            {
                case Tokens.Minus:
                    if (argType != TypeT.IntT && argType != TypeT.DoubleT)
                        SetError($"Wrong argument type in unary miuns operation - expected int or double, was {TypeToStr(argType)}", line);
                    break;
                case Tokens.BitNeg:
                    if (argType != TypeT.IntT)
                        SetError($"Wrong argument type in bit negation operation - expected int, was {TypeToStr(argType)}", line);
                    break;
                case Tokens.LogicNeg:
                    if (argType != TypeT.BoolT)
                        SetError($"Wrong argument type in logical negation operation - expected bool, was {TypeToStr(argType)}", line);
                    break;
                case Tokens.Int:
                    if (argType == TypeT.VoidT)
                        SetError($"Wrong argument type in cast to int operation", line);
                    break;
                case Tokens.Double:
                    if (argType == TypeT.VoidT)
                        SetError($"Wrong argument type in cast to double operation", line);
                    break;
            }
        }
        public override TypeT CheckType()
        {
            expression.CheckType();
            TypeT argType = expression.type;
            type = TypeT.VoidT;
            switch (kind)
            {
                case Tokens.Minus:
                    if(argType == TypeT.IntT || argType == TypeT.DoubleT)
                        type = argType;
                    break;
                case Tokens.BitNeg:
                    if (argType == TypeT.IntT)
                        type = argType;
                    break;
                case Tokens.LogicNeg:
                    if (argType == TypeT.BoolT)
                        type = argType;
                    break;
                case Tokens.Int:
                    if (argType != TypeT.VoidT)
                        type = TypeT.IntT;
                    break;
                case Tokens.Double:
                    if (argType != TypeT.VoidT)
                        type = TypeT.DoubleT;
                    break;
            }

            return type;
        }
        public override void GenCode()
        {
            expression.CheckType();
            expression.GenCode();

            switch (kind)
            {
                case Tokens.BitNeg:                   
                    Compiler.EmitCode("not");
                    break;
                case Tokens.LogicNeg:
                    Compiler.EmitCode("ldc.i4 0");
                    Compiler.EmitCode("ceq");
                    break;
                case Tokens.Int:
                    Compiler.EmitCode("conv.i4");         
                    break;
                case Tokens.Double:
                    Compiler.EmitCode("conv.r8");
                    break;
                case Tokens.Minus:
                    Compiler.EmitCode("neg");
                    break;
            }
        }
    }
    public class BinaryOpRelation : SyntaxTree
    {

        private SyntaxTree left;
        private SyntaxTree right;
        private Tokens kind;
        public BinaryOpRelation(SyntaxTree l, SyntaxTree r, Tokens k, int ln)
        {
            left = l;
            right = r;
            kind = k;
            line = ln;

            left.CheckType();
            right.CheckType();

            if (left.type == TypeT.BoolT && right.type == TypeT.BoolT) { }
            else if (left.type != TypeT.DoubleT && left.type != TypeT.IntT)
                SetError($"Wrong argument type in binary relation operation - expected int or double, was {TypeToStr(left.type)}", line);
            else if (right.type != TypeT.DoubleT && right.type != TypeT.IntT)
                SetError($"Wrong argument type in binary relation operation - expected int or double, was {TypeToStr(right.type)}", line);
        }
        public override TypeT CheckType()
        {
            type = TypeT.BoolT;           
            return type;
        }
        public override void GenCode()
        {
            left.CheckType();
            right.CheckType();

            left.GenCode();
            right.GenCode();

            if (left.type == TypeT.BoolT && right.type == TypeT.BoolT)
            {
                switch(kind)
                {
                    case Tokens.Equal:
                        Compiler.EmitCode("ceq");
                        break;
                    case Tokens.NotEqual:
                        Compiler.EmitCode("ceq");
                        Compiler.EmitCode("ldc.i4 0");
                        Compiler.EmitCode("ceq");                 
                        break;
                    default:
                        SetError("internal gencode error", line);
                        break;
                }
                return;
            }

            // uzgodnienie typów
            SetTypesOnStack(left.type, right.type);         

            switch (kind)
            {
                case Tokens.Equal:
                    Compiler.EmitCode("ceq");
                    break;
                case Tokens.NotEqual:
                    Compiler.EmitCode("ceq");
                    Compiler.EmitCode("ldc.i4 0");
                    Compiler.EmitCode("ceq");
                    break;
                case Tokens.More:
                    Compiler.EmitCode("cgt");
                    break;
                case Tokens.MoreEqual:
                    Compiler.EmitCode("clt");
                    Compiler.EmitCode("ldc.i4 0");
                    Compiler.EmitCode("ceq");
                    break;
                case Tokens.Less:
                    Compiler.EmitCode("clt");
                    break;
                case Tokens.LessEqual:
                    Compiler.EmitCode("cgt");
                    Compiler.EmitCode("ldc.i4 0");
                    Compiler.EmitCode("ceq");
                    break;
                default:
                    SetError("internal gencode error", line);
                    break;
            }
        }
    }

    public class BinaryOpBitwise : SyntaxTree
    {

        private SyntaxTree left;
        private SyntaxTree right;
        private Tokens kind;
        public BinaryOpBitwise(SyntaxTree l, SyntaxTree r, Tokens k, int ln)
        {
            left = l;
            right = r;
            kind = k;
            line = ln;

            left.CheckType();
            right.CheckType();
            if (left.type != TypeT.IntT || right.type != TypeT.IntT)
            {
                SetError($"Wrong argument type in bitwise operation - expected int, was {TypeToStr(left.type)} and {TypeToStr(right.type)}", line);
            }
        }
        public override TypeT CheckType()
        {
            type = TypeT.IntT;           
            return type;
        }
        public override void GenCode()
        {            
            left.GenCode();
            right.GenCode();

            switch (kind)
            {
                case Tokens.BitOr:
                    Compiler.EmitCode("or");
                    break;
                case Tokens.BitAnd:
                    Compiler.EmitCode("and");
                    break;
                default:
                    Console.WriteLine($"  line {line,3}:  internal gencode error");
                    errors++;
                    break;
            }
        }
    }

    public class BinaryOpLogical : SyntaxTree
    {

        private SyntaxTree left;
        private SyntaxTree right;
        private Tokens kind;
        public BinaryOpLogical(SyntaxTree l, SyntaxTree r, Tokens k, int ln)
        {
            left = l;
            right = r;
            kind = k;
            line = ln;

            left.CheckType();
            right.CheckType();
            if (left.type != TypeT.BoolT || right.type != TypeT.BoolT)
            {
                SetError($"Wrong argument type in logical operation - expected bool, was {TypeToStr(left.type)} and {TypeToStr(right.type)}", line);

            }
        }
        public override TypeT CheckType()
        {
            type = TypeT.BoolT;            
            return type;
        }
        public override void GenCode()
        {               
            left.GenCode();
            string labelIfFirstFalse = GetNewLabel();
            string labelEnd = GetNewLabel();

            //right.GenCode();

            switch (kind)
            {
                case Tokens.LogicOr:
                    
                    Compiler.EmitCode($"brfalse {labelIfFirstFalse}");
                    Compiler.EmitCode("nop");
                    Compiler.EmitCode("ldc.i4 1"); 
                    Compiler.EmitCode("nop");
                    Compiler.EmitCode($"br {labelEnd}");

                    Compiler.EmitCode($"{labelIfFirstFalse}: nop");
                    right.GenCode();
                    Compiler.EmitCode("nop");

                    Compiler.EmitCode($"{labelEnd}: nop");
                    break;
                case Tokens.LogicAnd:
                    Compiler.EmitCode($"brfalse {labelIfFirstFalse}");
                    Compiler.EmitCode("nop");
                    right.GenCode();
                    Compiler.EmitCode("nop");
                    Compiler.EmitCode($"br {labelEnd}");

                    Compiler.EmitCode($"{labelIfFirstFalse}: nop");
                    Compiler.EmitCode("ldc.i4 0");     
                    Compiler.EmitCode("nop");

                    Compiler.EmitCode($"{labelEnd}: nop");
                    break;
                default:
                    SetError("internal gencode error", line);
                    break;
            }
        }
    }

    public class BinaryOpAddMul : SyntaxTree
    {

        private SyntaxTree left;
        private SyntaxTree right;
        private Tokens kind;
        public BinaryOpAddMul(SyntaxTree l, SyntaxTree r, Tokens k, int ln)
        {
            left = l;
            right = r;
            kind = k;
            line = ln;

            left.CheckType();
            right.CheckType();

            if (left.type == TypeT.BoolT || left.type == TypeT.VoidT || right.type == TypeT.BoolT || left.type == TypeT.VoidT)
                SetError($"Wrong argument type in {kind.ToString()} operation - expected int or double, was {TypeToStr(left.type)} and {TypeToStr(right.type)}", line);

        }
        public override TypeT CheckType()
        {
            left.CheckType();
            right.CheckType();
            
            type = left.type == TypeT.IntT && right.type == TypeT.IntT ? TypeT.IntT : TypeT.DoubleT;
            return type;
        }

        public override void GenCode()
        {
            left.CheckType();
            right.CheckType();

            left.GenCode();
            right.GenCode();
            SetTypesOnStack(left.type, right.type);

            switch (kind)
            {
                case Tokens.Plus:
                    Compiler.EmitCode("add");
                    break;
                case Tokens.Minus:
                    Compiler.EmitCode("sub");
                    break;
                case Tokens.Multiplies:
                    Compiler.EmitCode("mul");
                    break;
                case Tokens.Divides:
                    Compiler.EmitCode("div");
                    break;
                default:
                    Console.WriteLine($"  line {line,3}:  internal gencode error");
                    errors++;
                    break;
            }
        }
    }

    public class BoolNumber : SyntaxTree
    {
        private bool val;
        public BoolNumber(bool v) { val = v; }
        public override TypeT CheckType()
        {
            type = TypeT.BoolT;
            return type;
        }
        public override void GenCode()
        {
            if(val)
                Compiler.EmitCode("ldc.i4 1");
            else
                Compiler.EmitCode("ldc.i4 0");
        }
    }

    public class IntNumber : SyntaxTree
    {
        private int val;
        public IntNumber(int v) { val = v; }
        public override TypeT CheckType()
        {
            type = TypeT.IntT;
            return type;
        }
        public override void GenCode()
        {
            Compiler.EmitCode("ldc.i4 {0}", val);
        }
    }

    public class DoubleNumber : SyntaxTree
    {
        private double val;
        public DoubleNumber(double v) { val = v; }
        public override TypeT CheckType()
        {
            type = TypeT.DoubleT;
            return type;
        }
        public override void GenCode()
        {
            Compiler.EmitCode(string.Format(System.Globalization.CultureInfo.InvariantCulture, "ldc.r8 {0}", val));
        }
    }

    public class Ident : SyntaxTree
    {
        private string id;
        public Ident(string s, int l)
        {
            string varName = string.Concat("_", s);
            if (!declaredVariables.ContainsKey(varName))
            {
                SetError($"Undeclared variable {s}.", l);
            }
            id = varName; line = l;
        }
        public override TypeT CheckType()
        {
            if (!declaredVariables.ContainsKey(id))
            {
                type = TypeT.VoidT;
            }
            else
                type = declaredVariables[id];
            return type;
        }
        public override void GenCode()
        {
            Compiler.EmitCode("ldloc {0}", id);
        }
    }

    public class WriteExprOp : SyntaxTree
    {
        SyntaxTree expr;
        public WriteExprOp(SyntaxTree e, int l)
        {
            expr = e;
            line = l;

            expr.CheckType();
            if (expr.type != TypeT.IntT && expr.type != TypeT.BoolT && expr.type != TypeT.DoubleT)
            {
                SetError($"Wrong argument type in write operation - expected int, double or bool.", line);
            }
        }

        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            expr.CheckType();
            

            switch (expr.type)
            {
                case TypeT.IntT:
                    expr.GenCode();
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(int32)");                   
                    break;
                case TypeT.DoubleT:                   

                    Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                    Compiler.EmitCode("ldstr \"{0:0.000000}\"");
                    expr.GenCode();
                    Compiler.EmitCode("box [mscorlib]System.Double");
                    Compiler.EmitCode("call string [mscorlib]System.String::Format(class [mscorlib]System.IFormatProvider,string,object)");
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");             
                    break;
                case TypeT.BoolT:
                    expr.GenCode();
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(bool)");
                    break;
            }
            Compiler.EmitCode("");
        }
    }

    public class WriteStrOp : SyntaxTree
    {
        private string val;
        public WriteStrOp(string s, int l)
        {
            val = s; line = l;
        }
        public override TypeT CheckType()
        {
            return TypeT.VoidT; 
        }

        public override void GenCode()
        {
            Compiler.EmitCode("ldstr {0}",val);
            Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
            Compiler.EmitCode("");
        }
    }

    public class ReadOp : SyntaxTree
    {
        string variableName;

        public ReadOp(string name, int ln)
        {
            variableName = string.Concat("_", name);
            line = ln;

            if (!declaredVariables.ContainsKey(variableName))
            {
                SetError($"Variable {name} undeclared", line);
            }
            TypeT varType = declaredVariables[variableName];
            if (varType != TypeT.IntT && varType != TypeT.BoolT && varType != TypeT.DoubleT)
            {
                SetError($"Wrong variable type in read operation", line);
            }
        }
        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            
            TypeT varType = declaredVariables[variableName];
            Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
            Compiler.EmitCode("ldloca {0}", variableName);
            switch (varType)
            {
                case TypeT.IntT:
                    Compiler.EmitCode("call bool [mscorlib]System.Int32::TryParse(string, int32&)");
                    break;
                case TypeT.DoubleT:
                    Compiler.EmitCode("call bool [mscorlib]System.Double::TryParse(string, float64&)");
                    break;
                case TypeT.BoolT:
                    Compiler.EmitCode("call bool [mscorlib]System.Boolean::TryParse(string, bool&)");
                    break;
            }
            Compiler.EmitCode("pop");

        }
    }

    public class AssignOp : SyntaxTree
    {
        string variableName;
       
        SyntaxTree rightExpr;

        public AssignOp(SyntaxTree r, string name, int ln)
        {
            line = ln;
            rightExpr = r;
            variableName = string.Concat("_", name);

            if (!declaredVariables.ContainsKey(variableName))
            {
                SetError($"Variable {name} undeclared", line);
            }
            rightExpr.CheckType();
            TypeT varType = declaredVariables[variableName];

            if (varType == TypeT.IntT || varType == TypeT.BoolT)
            {
                if (varType != rightExpr.type)
                {
                    SetError($"Wrong type in assigment - expected {TypeToStr(varType)}, was {TypeToStr(rightExpr.type)}", line);
                }
            }
            else if (varType == TypeT.DoubleT)
            {
                if (rightExpr.type != TypeT.DoubleT && rightExpr.type != TypeT.IntT)
                {
                    SetError($"Wrong type in assigment - expected {TypeToStr(varType)}, was {TypeToStr(rightExpr.type)}", line);
                }
            }
        }

        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            if (declaredVariables.ContainsKey(variableName))
            {
                type = declaredVariables[variableName];
            }
            return type;
        }

        public override void GenCode()
        {

            TypeT varType = declaredVariables[variableName];
            rightExpr.CheckType();
            rightExpr.GenCode();

            if (varType == TypeT.IntT || varType == TypeT.BoolT)
            {              
                Compiler.EmitCode("stloc {0}", variableName);
                
            }

            if(varType == TypeT.DoubleT)
            {
                if (rightExpr.type == TypeT.IntT)
                {
                    Compiler.EmitCode("conv.r8");
                }
                Compiler.EmitCode("stloc {0}", variableName);
            }
            Compiler.EmitCode("ldloc {0}", variableName);
          
        }

    }

    public class IfOp : SyntaxTree
    {
        SyntaxTree condition;
        SyntaxTree stmt;

        public IfOp(SyntaxTree c, SyntaxTree s, int ln)
        {
            line = ln;
            condition = c;
            stmt = s;

            condition.CheckType();
            if (condition.type != TypeT.BoolT)
            {
                SetError($"Wrong condition type in if statement- expected {TypeToStr(TypeT.BoolT)}, was {TypeToStr(condition.type)}", line);
            }
        }
        public override TypeT CheckType()
        {
            type = TypeT.VoidT;           
            return type;
        }
        public override void GenCode()
        {           
            condition.GenCode();
            string label = GetNewLabel();
            Compiler.EmitCode("brfalse {0}", label);
            Compiler.EmitCode("nop");
            stmt.GenCode();
            Compiler.EmitCode($"{label}: nop");
        }
    }

    public class IfElseOp : SyntaxTree
    {
        SyntaxTree condition;
        SyntaxTree stmt;
        SyntaxTree elseStmt;

        public IfElseOp(SyntaxTree c, SyntaxTree s, SyntaxTree e, int ln)
        {
            line = ln;
            condition = c;
            stmt = s;
            elseStmt = e;

            condition.CheckType();
            if (condition.type != TypeT.BoolT)
            {
                SetError($"Wrong condition type in if statement - expected {TypeToStr(TypeT.BoolT)}, was {TypeToStr(condition.type)}", line);
            }

        }
        public override TypeT CheckType()
        {
            type = TypeT.VoidT;                       
            return type;
        }
        public override void GenCode()
        {
            condition.GenCode();
            string labelIfFalse = GetNewLabel();
            Compiler.EmitCode("brfalse {0}", labelIfFalse);
            Compiler.EmitCode("nop");
            stmt.GenCode();
            Compiler.EmitCode("nop");
            string labelIfTrue = GetNewLabel();
            Compiler.EmitCode("br {0}", labelIfTrue);
            Compiler.EmitCode($"{labelIfFalse}: nop");
            elseStmt.GenCode();
            Compiler.EmitCode($"{labelIfTrue}: nop");

        }
    }

    public class WhileOp : SyntaxTree
    {
        SyntaxTree condition;
        SyntaxTree stmt;

        public WhileOp(SyntaxTree c, SyntaxTree s, int ln)
        {
            line = ln;
            condition = c;
            stmt = s;

            condition.CheckType();
            if (condition.type != TypeT.BoolT)
            {
                SetError($"Wrong condition type in while loop - expected {TypeToStr(TypeT.BoolT)}, was {TypeToStr(condition.type)}", line);
            }
        }

        public override TypeT CheckType()
        {
            type = TypeT.VoidT;          
            return type;
        }

        public override void GenCode()
        {
            string labelCondition = GetNewLabel();
            string labelStmt = GetNewLabel();

            Compiler.EmitCode($"br {labelCondition}");

            Compiler.EmitCode($"{labelStmt}: nop");
            stmt.GenCode();
            Compiler.EmitCode("nop");

            Compiler.EmitCode($"{labelCondition}: nop");
            condition.GenCode();
            Compiler.EmitCode($"brtrue {labelStmt}");

        }
    }

    public class SingleExprInstruction : SyntaxTree
    {
        SyntaxTree expr;

        public SingleExprInstruction(SyntaxTree e, int ln)
        {            
            expr = e;
            line = ln;

            expr.CheckType();
        }

        public override TypeT CheckType()
        {           
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            expr.GenCode();
            Compiler.EmitCode("pop");

        }
    }

    

    public class ReturnOp : SyntaxTree
    {

        public ReturnOp(int ln)
        {
            line = ln;
        }

        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            Compiler.EmitCode("br IL_{0}", returnLabel);

        }
    }

    public class ProgramTree : SyntaxTree
    {
        SyntaxTree exprList;

        public ProgramTree(SyntaxTree el, int ln)
        {
            line = ln;
            exprList = el;

            if(exprList != null)
                exprList.CheckType();

        }
        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            if (exprList != null)
                exprList.GenCode();
        }
    }

    public class ExprList : SyntaxTree
    {
        SyntaxTree exprList;
        SyntaxTree stmt;

        public ExprList(SyntaxTree s, SyntaxTree el, int ln)
        {
            line = ln;
            exprList = el;
            stmt = s;
            stmt.CheckType();
            exprList.CheckType();
           
        }
        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            stmt.GenCode();
            exprList.GenCode();
        }
    }

    public class EmptyBrackets : SyntaxTree
    {
        public EmptyBrackets(int ln)
        {
            line = ln;

        }
        public override TypeT CheckType()
        {
            type = TypeT.VoidT;
            return type;
        }

        public override void GenCode()
        {
            EmitCode("nop");
            return;
        }
    }



}
