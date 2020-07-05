
%namespace GardensPoint

%union
{
public string  val;
public Compiler.SyntaxTree node;
}

%token Program If Else While Read Write Return Int Double Bool Assign OpenPar ClosePar OpenBr CloseBr Semicolon Endl Eof Error Plus Minus Multiplies Divides LogicOr LogicAnd BitOr BitAnd Equal NotEqual More MoreEqual Less LessEqual LogicNeg BitNeg 

%token <val> Ident IntNumber DoubleNumber Str True False

%type <node> expr exprRelation exprAdd exprMul exprBit exprUn value write_stmt read_stmt return_stmt if_stmt block expr_list statements while_stmt exprLogic expr_instruction program

%%
start		: program Eof 
				{
				YYACCEPT;
				}
			| error Eof 
				{ 
				++Compiler.errors;
				Console.WriteLine("syntax error"); 
				yyerrok();
				YYABORT;
				}
			| Eof
				{ 
				++Compiler.errors;
				Console.WriteLine("syntax error - unexpected EOF"); 
				yyerrok();
				YYABORT;
				}
			;

program	  	: Program OpenBr CloseBr
				{
				Compiler.root = new Compiler.ProgramTree(null, Compiler.lineno);
				}
			| Program OpenBr dec_list CloseBr
				{
				Compiler.root = new Compiler.ProgramTree(null, Compiler.lineno);
				}
			| Program OpenBr expr_list CloseBr
				{
				Compiler.root = new Compiler.ProgramTree($3, Compiler.lineno);
				}
			| Program OpenBr dec_list expr_list CloseBr	
				{
				Compiler.root = new Compiler.ProgramTree($4, Compiler.lineno);
				}
			| Program OpenBr dec_list Eof 
				{
				Console.WriteLine("syntax error - unexpected EOF, missing close bracket"); 
				++Compiler.errors;
				yyerrok();
				YYABORT;
				}
			| Program OpenBr expr_list Eof
				{
				Console.WriteLine("syntax error - unexpected EOF, missing close bracket"); 
				++Compiler.errors;
				yyerrok();
				YYABORT;
				}
			| Program OpenBr dec_list expr_list Eof
				{
				Console.WriteLine("syntax error - unexpected EOF, missing close bracket"); 
				++Compiler.errors;
				yyerrok();
				YYABORT;
				}
			;


dec_list	: declaration
			| declaration dec_list 
			;

declaration	: Int Ident Semicolon
				{
				Compiler.DeclareNewVariable($2, Tokens.Int, Compiler.lineno);
				}
			| Double Ident Semicolon
				{
				Compiler.DeclareNewVariable($2, Tokens.Double, Compiler.lineno);
				}
			| Bool Ident Semicolon
				{
				Compiler.DeclareNewVariable($2, Tokens.Bool, Compiler.lineno);
				}
			;

block		: OpenBr expr_list CloseBr 
				{
				$$ = $2;
				}
			| OpenBr CloseBr
				{
				Compiler.EmptyBrackets n = new Compiler.EmptyBrackets(Compiler.lineno);
				$$ = n;
				}
			;

expr_list	: statements
				{
				$$ = $1;
				}
			| statements expr_list 	
				{
				Compiler.ExprList n = new Compiler.ExprList($1, $2, Compiler.lineno);
				$$ = n;
				}
			;


statements	: if_stmt 
				{
				$$ = $1;
				}
			| while_stmt
				{
				$$ = $1;
				}
			| read_stmt Semicolon
				{
				$$ = $1;
				}
			| write_stmt Semicolon
				{
				$$ = $1;
				}
			| expr_instruction
				{
				$$ = $1;
				}
			| return_stmt Semicolon
				{
				$$ = $1;
				}
			| block
				{
				$$ = $1;
				}
			;		

if_stmt		: If OpenPar expr ClosePar statements Else statements
				{
				Compiler.IfElseOp n = new Compiler.IfElseOp($3, $5, $7, Compiler.lineno);
				$$ = n;
				}
			| If OpenPar expr ClosePar statements 
				{
				Compiler.IfOp n = new Compiler.IfOp($3, $5, Compiler.lineno);
				$$ = n;
				}
			;



while_stmt	: While OpenPar expr ClosePar statements
				{
				Compiler.WhileOp n = new Compiler.WhileOp($3, $5, Compiler.lineno);
				$$ = n;
				}
			;

read_stmt	: Read Ident 
				{
				Compiler.ReadOp n = new Compiler.ReadOp($2, Compiler.lineno);
				$$ = n;
				}
			;

write_stmt	: Write expr  
				{
				Compiler.WriteExprOp n = new Compiler.WriteExprOp($2, Compiler.lineno);
				$$ = n;
				}
			| Write Str 
				{
				Compiler.WriteStrOp n = new Compiler.WriteStrOp($2, Compiler.lineno);
				$$ = n;
				}
			;

return_stmt	: Return
				{
				Compiler.ReturnOp n = new Compiler.ReturnOp(Compiler.lineno);
				$$ = n;
				}
			;

expr_instruction	: expr Semicolon
						{
						Compiler.SingleExprInstruction n = new Compiler.SingleExprInstruction($1, Compiler.lineno);
						$$ = n;
						}
					;


expr		: Ident Assign expr 
				{
				Compiler.AssignOp n = new Compiler.AssignOp($3, $1, Compiler.lineno);
				$$ = n;
				}	
			| exprLogic 
				{
				$$ = $1;
				}			
			;

exprLogic	: exprLogic LogicOr exprRelation
				{
				Compiler.BinaryOpLogical n = new Compiler.BinaryOpLogical($1, $3, Tokens.LogicOr, Compiler.lineno);
				$$ = n;
				}
			| exprLogic LogicAnd exprRelation 
				{
				Compiler.BinaryOpLogical n = new Compiler.BinaryOpLogical($1, $3, Tokens.LogicAnd, Compiler.lineno);
				$$ = n;
				}
			| exprRelation
				{
				$$ = $1;
				}
			;

exprRelation : exprRelation Equal exprAdd 
				{
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation($1, $3, Tokens.Equal, Compiler.lineno);
				$$ = n;
				}
			| exprRelation NotEqual exprAdd 
				{
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation($1, $3, Tokens.NotEqual, Compiler.lineno);
				$$ = n;
				}
			| exprRelation More exprAdd 
				{
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation($1, $3, Tokens.More, Compiler.lineno);
				$$ = n;
				}
			| exprRelation MoreEqual exprAdd 
				{
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation($1, $3, Tokens.MoreEqual, Compiler.lineno);
				$$ = n;
				}
			| exprRelation Less exprAdd 
				{
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation($1, $3, Tokens.Less, Compiler.lineno);
				$$ = n;
				}
			| exprRelation LessEqual exprAdd 
				{
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation($1, $3, Tokens.LessEqual, Compiler.lineno);
				$$ = n;
				}
			| exprAdd
				{
				$$ = $1;
				}
			;

exprAdd 	: exprAdd Plus exprMul 
				{
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul($1, $3, Tokens.Plus, Compiler.lineno);
				$$ = n;
				}
			| exprAdd Minus exprMul 
				{
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul($1, $3, Tokens.Minus, Compiler.lineno);
				$$ = n;
				}
			| exprMul
				{
				$$ = $1;
				}
			;

exprMul		: exprMul Multiplies exprBit 
				{
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul($1, $3, Tokens.Multiplies, Compiler.lineno);
				$$ = n;
				}
			| exprMul Divides exprBit 
				{
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul($1, $3, Tokens.Divides, Compiler.lineno);
				$$ = n;
				}
			| exprBit 
				{
				$$ = $1;
				}
			;
 
exprBit		: exprBit BitOr exprUn 
				{
				Compiler.BinaryOpBitwise n = new Compiler.BinaryOpBitwise($1, $3, Tokens.BitOr, Compiler.lineno);
				$$ = n;
				}
			| exprBit BitAnd exprUn 
				{
				Compiler.BinaryOpBitwise n = new Compiler.BinaryOpBitwise($1, $3, Tokens.BitAnd, Compiler.lineno);
				$$ = n;
				}
			| exprUn 
				{
				$$ = $1;
				}
			;

exprUn		: Minus exprUn 
				{
				Compiler.UnaryOp n = new Compiler.UnaryOp($2, Tokens.Minus, Compiler.lineno);
				$$ = n;
				}
			| BitNeg exprUn
				{
				Compiler.UnaryOp n = new Compiler.UnaryOp($2, Tokens.BitNeg, Compiler.lineno);
				$$ = n;
				}
			| LogicNeg exprUn 
				{
				Compiler.UnaryOp n = new Compiler.UnaryOp($2, Tokens.LogicNeg, Compiler.lineno);
				$$ = n;
				}
			| OpenPar Int ClosePar exprUn 
				{
				Compiler.UnaryOp n = new Compiler.UnaryOp($4, Tokens.Int, Compiler.lineno);
				$$ = n;
				}
			| OpenPar Double ClosePar exprUn 
				{
				Compiler.UnaryOp n = new Compiler.UnaryOp($4, Tokens.Double, Compiler.lineno);
				$$ = n;
				}
			| OpenPar expr ClosePar 
				{
				$$ = $2;
				}
			| value
				{
				$$ = $1;
				}
			;
			
value		: IntNumber 
				{
				Compiler.IntNumber n = new Compiler.IntNumber(int.Parse($1));
				$$ = n;
				}
			| DoubleNumber 
				{
				double d = double.Parse($1,System.Globalization.CultureInfo.InvariantCulture);
				Compiler.DoubleNumber n = new Compiler.DoubleNumber(d);
				$$ = n;
				}
			| True 
				{
				Compiler.BoolNumber n = new Compiler.BoolNumber(true);
				$$ = n;
				}
			| False 
				{
				Compiler.BoolNumber n = new Compiler.BoolNumber(false);
				$$ = n;
				}
			| Ident	
				{
				Compiler.Ident n = new Compiler.Ident($1, Compiler.lineno);
				$$ = n;
				}
			;
 
%%

public Parser(Scanner scanner) : base(scanner) { }