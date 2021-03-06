// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  WATERMELON007
// DateTime: 7/6/2020 1:48:40 AM
// UserName: moska
// Input file <kompilator.y - 7/6/2020 1:48:20 AM>

// options: conflicts lines gplex conflicts

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Program=4,If=5,Else=6,
    While=7,Read=8,Write=9,Return=10,Int=11,Double=12,
    Bool=13,Assign=14,OpenPar=15,ClosePar=16,OpenBr=17,CloseBr=18,
    Semicolon=19,Endl=20,Eof=21,Error=22,Plus=23,Minus=24,
    Multiplies=25,Divides=26,LogicOr=27,LogicAnd=28,BitOr=29,BitAnd=30,
    Equal=31,NotEqual=32,More=33,MoreEqual=34,Less=35,LessEqual=36,
    LogicNeg=37,BitNeg=38,Ident=39,IntNumber=40,DoubleNumber=41,Str=42,
    True=43,False=44};

public struct ValueType
#line 5 "kompilator.y"
{
public string  val;
public Compiler.SyntaxTree node;
}
#line default
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[69];
  private static State[] states = new State[125];
  private static string[] nonTerms = new string[] {
      "expr", "exprRelation", "exprAdd", "exprMul", "exprBit", "exprUn", "value", 
      "write_stmt", "read_stmt", "return_stmt", "if_stmt", "block", "expr_list", 
      "statements", "while_stmt", "exprLogic", "expr_instruction", "program", 
      "start", "$accept", "dec_list", "declaration", };

  static Parser() {
    states[0] = new State(new int[]{4,5,2,122,21,124},new int[]{-19,1,-18,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{21,4});
    states[4] = new State(-2);
    states[5] = new State(new int[]{17,6});
    states[6] = new State(new int[]{18,7,11,113,12,116,13,119,5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104},new int[]{-21,8,-13,108,-22,111,-14,14,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[7] = new State(-5);
    states[8] = new State(new int[]{18,9,21,13,5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104},new int[]{-13,10,-14,14,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[9] = new State(-6);
    states[10] = new State(new int[]{18,11,21,12});
    states[11] = new State(-8);
    states[12] = new State(-11);
    states[13] = new State(-9);
    states[14] = new State(new int[]{5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104,18,-19,21,-19},new int[]{-13,15,-14,14,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[15] = new State(-20);
    states[16] = new State(-21);
    states[17] = new State(new int[]{15,18});
    states[18] = new State(new int[]{39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67},new int[]{-1,19,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[19] = new State(new int[]{16,20});
    states[20] = new State(new int[]{5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104},new int[]{-14,21,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[21] = new State(new int[]{6,22,5,-29,7,-29,8,-29,9,-29,39,-29,24,-29,38,-29,37,-29,15,-29,40,-29,41,-29,43,-29,44,-29,10,-29,17,-29,18,-29,21,-29});
    states[22] = new State(new int[]{5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104},new int[]{-14,23,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[23] = new State(-28);
    states[24] = new State(-22);
    states[25] = new State(new int[]{15,26});
    states[26] = new State(new int[]{39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67},new int[]{-1,27,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[27] = new State(new int[]{16,28});
    states[28] = new State(new int[]{5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104},new int[]{-14,29,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[29] = new State(-30);
    states[30] = new State(new int[]{19,31});
    states[31] = new State(-23);
    states[32] = new State(new int[]{39,33});
    states[33] = new State(-31);
    states[34] = new State(new int[]{19,35});
    states[35] = new State(-24);
    states[36] = new State(new int[]{42,38,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67},new int[]{-1,37,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[37] = new State(-32);
    states[38] = new State(-33);
    states[39] = new State(new int[]{14,40,29,-68,30,-68,25,-68,26,-68,23,-68,24,-68,31,-68,32,-68,33,-68,34,-68,35,-68,36,-68,27,-68,28,-68,19,-68,16,-68});
    states[40] = new State(new int[]{39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67},new int[]{-1,41,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[41] = new State(-36);
    states[42] = new State(new int[]{27,43,28,95,19,-37,16,-37});
    states[43] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-2,44,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[44] = new State(new int[]{31,45,32,75,33,86,34,88,35,90,36,92,27,-38,28,-38,19,-38,16,-38});
    states[45] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-3,46,-4,85,-5,84,-6,83,-7,63});
    states[46] = new State(new int[]{23,47,24,77,31,-41,32,-41,33,-41,34,-41,35,-41,36,-41,27,-41,28,-41,19,-41,16,-41});
    states[47] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-4,48,-5,84,-6,83,-7,63});
    states[48] = new State(new int[]{25,49,26,79,23,-48,24,-48,31,-48,32,-48,33,-48,34,-48,35,-48,36,-48,27,-48,28,-48,19,-48,16,-48});
    states[49] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-5,50,-6,83,-7,63});
    states[50] = new State(new int[]{29,51,30,81,25,-51,26,-51,23,-51,24,-51,31,-51,32,-51,33,-51,34,-51,35,-51,36,-51,27,-51,28,-51,19,-51,16,-51});
    states[51] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,52,-7,63});
    states[52] = new State(-54);
    states[53] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,54,-7,63});
    states[54] = new State(-57);
    states[55] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,56,-7,63});
    states[56] = new State(-58);
    states[57] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,58,-7,63});
    states[58] = new State(-59);
    states[59] = new State(new int[]{11,60,12,69,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67},new int[]{-1,72,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[60] = new State(new int[]{16,61});
    states[61] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,62,-7,63});
    states[62] = new State(-60);
    states[63] = new State(-63);
    states[64] = new State(-64);
    states[65] = new State(-65);
    states[66] = new State(-66);
    states[67] = new State(-67);
    states[68] = new State(-68);
    states[69] = new State(new int[]{16,70});
    states[70] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,71,-7,63});
    states[71] = new State(-61);
    states[72] = new State(new int[]{16,73});
    states[73] = new State(-62);
    states[74] = new State(new int[]{31,45,32,75,33,86,34,88,35,90,36,92,27,-40,28,-40,19,-40,16,-40});
    states[75] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-3,76,-4,85,-5,84,-6,83,-7,63});
    states[76] = new State(new int[]{23,47,24,77,31,-42,32,-42,33,-42,34,-42,35,-42,36,-42,27,-42,28,-42,19,-42,16,-42});
    states[77] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-4,78,-5,84,-6,83,-7,63});
    states[78] = new State(new int[]{25,49,26,79,23,-49,24,-49,31,-49,32,-49,33,-49,34,-49,35,-49,36,-49,27,-49,28,-49,19,-49,16,-49});
    states[79] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-5,80,-6,83,-7,63});
    states[80] = new State(new int[]{29,51,30,81,25,-52,26,-52,23,-52,24,-52,31,-52,32,-52,33,-52,34,-52,35,-52,36,-52,27,-52,28,-52,19,-52,16,-52});
    states[81] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-6,82,-7,63});
    states[82] = new State(-55);
    states[83] = new State(-56);
    states[84] = new State(new int[]{29,51,30,81,25,-53,26,-53,23,-53,24,-53,31,-53,32,-53,33,-53,34,-53,35,-53,36,-53,27,-53,28,-53,19,-53,16,-53});
    states[85] = new State(new int[]{25,49,26,79,23,-50,24,-50,31,-50,32,-50,33,-50,34,-50,35,-50,36,-50,27,-50,28,-50,19,-50,16,-50});
    states[86] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-3,87,-4,85,-5,84,-6,83,-7,63});
    states[87] = new State(new int[]{23,47,24,77,31,-43,32,-43,33,-43,34,-43,35,-43,36,-43,27,-43,28,-43,19,-43,16,-43});
    states[88] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-3,89,-4,85,-5,84,-6,83,-7,63});
    states[89] = new State(new int[]{23,47,24,77,31,-44,32,-44,33,-44,34,-44,35,-44,36,-44,27,-44,28,-44,19,-44,16,-44});
    states[90] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-3,91,-4,85,-5,84,-6,83,-7,63});
    states[91] = new State(new int[]{23,47,24,77,31,-45,32,-45,33,-45,34,-45,35,-45,36,-45,27,-45,28,-45,19,-45,16,-45});
    states[92] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-3,93,-4,85,-5,84,-6,83,-7,63});
    states[93] = new State(new int[]{23,47,24,77,31,-46,32,-46,33,-46,34,-46,35,-46,36,-46,27,-46,28,-46,19,-46,16,-46});
    states[94] = new State(new int[]{23,47,24,77,31,-47,32,-47,33,-47,34,-47,35,-47,36,-47,27,-47,28,-47,19,-47,16,-47});
    states[95] = new State(new int[]{24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,39,68},new int[]{-2,96,-3,94,-4,85,-5,84,-6,83,-7,63});
    states[96] = new State(new int[]{31,45,32,75,33,86,34,88,35,90,36,92,27,-39,28,-39,19,-39,16,-39});
    states[97] = new State(-25);
    states[98] = new State(new int[]{19,99});
    states[99] = new State(-35);
    states[100] = new State(new int[]{19,101});
    states[101] = new State(-26);
    states[102] = new State(-34);
    states[103] = new State(-27);
    states[104] = new State(new int[]{18,107,5,17,7,25,8,32,9,36,39,39,24,53,38,55,37,57,15,59,40,64,41,65,43,66,44,67,10,102,17,104},new int[]{-13,105,-14,14,-11,16,-15,24,-9,30,-8,34,-17,97,-1,98,-16,42,-2,74,-3,94,-4,85,-5,84,-6,83,-7,63,-10,100,-12,103});
    states[105] = new State(new int[]{18,106});
    states[106] = new State(-17);
    states[107] = new State(-18);
    states[108] = new State(new int[]{18,109,21,110});
    states[109] = new State(-7);
    states[110] = new State(-10);
    states[111] = new State(new int[]{11,113,12,116,13,119,18,-12,21,-12,5,-12,7,-12,8,-12,9,-12,39,-12,24,-12,38,-12,37,-12,15,-12,40,-12,41,-12,43,-12,44,-12,10,-12,17,-12},new int[]{-21,112,-22,111});
    states[112] = new State(-13);
    states[113] = new State(new int[]{39,114});
    states[114] = new State(new int[]{19,115});
    states[115] = new State(-14);
    states[116] = new State(new int[]{39,117});
    states[117] = new State(new int[]{19,118});
    states[118] = new State(-15);
    states[119] = new State(new int[]{39,120});
    states[120] = new State(new int[]{19,121});
    states[121] = new State(-16);
    states[122] = new State(new int[]{21,123});
    states[123] = new State(-3);
    states[124] = new State(-4);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-20, new int[]{-19,3});
    rules[2] = new Rule(-19, new int[]{-18,21});
    rules[3] = new Rule(-19, new int[]{2,21});
    rules[4] = new Rule(-19, new int[]{21});
    rules[5] = new Rule(-18, new int[]{4,17,18});
    rules[6] = new Rule(-18, new int[]{4,17,-21,18});
    rules[7] = new Rule(-18, new int[]{4,17,-13,18});
    rules[8] = new Rule(-18, new int[]{4,17,-21,-13,18});
    rules[9] = new Rule(-18, new int[]{4,17,-21,21});
    rules[10] = new Rule(-18, new int[]{4,17,-13,21});
    rules[11] = new Rule(-18, new int[]{4,17,-21,-13,21});
    rules[12] = new Rule(-21, new int[]{-22});
    rules[13] = new Rule(-21, new int[]{-22,-21});
    rules[14] = new Rule(-22, new int[]{11,39,19});
    rules[15] = new Rule(-22, new int[]{12,39,19});
    rules[16] = new Rule(-22, new int[]{13,39,19});
    rules[17] = new Rule(-12, new int[]{17,-13,18});
    rules[18] = new Rule(-12, new int[]{17,18});
    rules[19] = new Rule(-13, new int[]{-14});
    rules[20] = new Rule(-13, new int[]{-14,-13});
    rules[21] = new Rule(-14, new int[]{-11});
    rules[22] = new Rule(-14, new int[]{-15});
    rules[23] = new Rule(-14, new int[]{-9,19});
    rules[24] = new Rule(-14, new int[]{-8,19});
    rules[25] = new Rule(-14, new int[]{-17});
    rules[26] = new Rule(-14, new int[]{-10,19});
    rules[27] = new Rule(-14, new int[]{-12});
    rules[28] = new Rule(-11, new int[]{5,15,-1,16,-14,6,-14});
    rules[29] = new Rule(-11, new int[]{5,15,-1,16,-14});
    rules[30] = new Rule(-15, new int[]{7,15,-1,16,-14});
    rules[31] = new Rule(-9, new int[]{8,39});
    rules[32] = new Rule(-8, new int[]{9,-1});
    rules[33] = new Rule(-8, new int[]{9,42});
    rules[34] = new Rule(-10, new int[]{10});
    rules[35] = new Rule(-17, new int[]{-1,19});
    rules[36] = new Rule(-1, new int[]{39,14,-1});
    rules[37] = new Rule(-1, new int[]{-16});
    rules[38] = new Rule(-16, new int[]{-16,27,-2});
    rules[39] = new Rule(-16, new int[]{-16,28,-2});
    rules[40] = new Rule(-16, new int[]{-2});
    rules[41] = new Rule(-2, new int[]{-2,31,-3});
    rules[42] = new Rule(-2, new int[]{-2,32,-3});
    rules[43] = new Rule(-2, new int[]{-2,33,-3});
    rules[44] = new Rule(-2, new int[]{-2,34,-3});
    rules[45] = new Rule(-2, new int[]{-2,35,-3});
    rules[46] = new Rule(-2, new int[]{-2,36,-3});
    rules[47] = new Rule(-2, new int[]{-3});
    rules[48] = new Rule(-3, new int[]{-3,23,-4});
    rules[49] = new Rule(-3, new int[]{-3,24,-4});
    rules[50] = new Rule(-3, new int[]{-4});
    rules[51] = new Rule(-4, new int[]{-4,25,-5});
    rules[52] = new Rule(-4, new int[]{-4,26,-5});
    rules[53] = new Rule(-4, new int[]{-5});
    rules[54] = new Rule(-5, new int[]{-5,29,-6});
    rules[55] = new Rule(-5, new int[]{-5,30,-6});
    rules[56] = new Rule(-5, new int[]{-6});
    rules[57] = new Rule(-6, new int[]{24,-6});
    rules[58] = new Rule(-6, new int[]{38,-6});
    rules[59] = new Rule(-6, new int[]{37,-6});
    rules[60] = new Rule(-6, new int[]{15,11,16,-6});
    rules[61] = new Rule(-6, new int[]{15,12,16,-6});
    rules[62] = new Rule(-6, new int[]{15,-1,16});
    rules[63] = new Rule(-6, new int[]{-7});
    rules[64] = new Rule(-7, new int[]{40});
    rules[65] = new Rule(-7, new int[]{41});
    rules[66] = new Rule(-7, new int[]{43});
    rules[67] = new Rule(-7, new int[]{44});
    rules[68] = new Rule(-7, new int[]{39});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // start -> program, Eof
#line 18 "kompilator.y"
    {
				YYAccept();
				}
#line default
        break;
      case 3: // start -> error, Eof
#line 22 "kompilator.y"
    { 
				++Compiler.errors;
				Console.WriteLine("syntax error"); 
				yyerrok();
				YYAbort();
				}
#line default
        break;
      case 4: // start -> Eof
#line 29 "kompilator.y"
    { 
				++Compiler.errors;
				Console.WriteLine("syntax error - unexpected EOF"); 
				yyerrok();
				YYAbort();
				}
#line default
        break;
      case 5: // program -> Program, OpenBr, CloseBr
#line 38 "kompilator.y"
    {
				Compiler.root = new Compiler.ProgramTree(null, Compiler.lineno);
				}
#line default
        break;
      case 6: // program -> Program, OpenBr, dec_list, CloseBr
#line 42 "kompilator.y"
    {
				Compiler.root = new Compiler.ProgramTree(null, Compiler.lineno);
				}
#line default
        break;
      case 7: // program -> Program, OpenBr, expr_list, CloseBr
#line 46 "kompilator.y"
    {
				Compiler.root = new Compiler.ProgramTree(ValueStack[ValueStack.Depth-2].node, Compiler.lineno);
				}
#line default
        break;
      case 8: // program -> Program, OpenBr, dec_list, expr_list, CloseBr
#line 50 "kompilator.y"
    {
				Compiler.root = new Compiler.ProgramTree(ValueStack[ValueStack.Depth-2].node, Compiler.lineno);
				}
#line default
        break;
      case 9: // program -> Program, OpenBr, dec_list, Eof
#line 54 "kompilator.y"
    {
				Console.WriteLine("syntax error - unexpected EOF, missing close bracket"); 
				++Compiler.errors;
				yyerrok();
				YYAbort();
				}
#line default
        break;
      case 10: // program -> Program, OpenBr, expr_list, Eof
#line 61 "kompilator.y"
    {
				Console.WriteLine("syntax error - unexpected EOF, missing close bracket"); 
				++Compiler.errors;
				yyerrok();
				YYAbort();
				}
#line default
        break;
      case 11: // program -> Program, OpenBr, dec_list, expr_list, Eof
#line 68 "kompilator.y"
    {
				Console.WriteLine("syntax error - unexpected EOF, missing close bracket"); 
				++Compiler.errors;
				yyerrok();
				YYAbort();
				}
#line default
        break;
      case 14: // declaration -> Int, Ident, Semicolon
#line 82 "kompilator.y"
    {
				Compiler.DeclareNewVariable(ValueStack[ValueStack.Depth-2].val, Tokens.Int, Compiler.lineno);
				}
#line default
        break;
      case 15: // declaration -> Double, Ident, Semicolon
#line 86 "kompilator.y"
    {
				Compiler.DeclareNewVariable(ValueStack[ValueStack.Depth-2].val, Tokens.Double, Compiler.lineno);
				}
#line default
        break;
      case 16: // declaration -> Bool, Ident, Semicolon
#line 90 "kompilator.y"
    {
				Compiler.DeclareNewVariable(ValueStack[ValueStack.Depth-2].val, Tokens.Bool, Compiler.lineno);
				}
#line default
        break;
      case 17: // block -> OpenBr, expr_list, CloseBr
#line 96 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-2].node;
				}
#line default
        break;
      case 18: // block -> OpenBr, CloseBr
#line 100 "kompilator.y"
    {
				Compiler.EmptyBrackets n = new Compiler.EmptyBrackets(Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 19: // expr_list -> statements
#line 107 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 20: // expr_list -> statements, expr_list
#line 111 "kompilator.y"
    {
				Compiler.ExprList n = new Compiler.ExprList(ValueStack[ValueStack.Depth-2].node, ValueStack[ValueStack.Depth-1].node, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 21: // statements -> if_stmt
#line 119 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 22: // statements -> while_stmt
#line 123 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 23: // statements -> read_stmt, Semicolon
#line 127 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-2].node;
				}
#line default
        break;
      case 24: // statements -> write_stmt, Semicolon
#line 131 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-2].node;
				}
#line default
        break;
      case 25: // statements -> expr_instruction
#line 135 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 26: // statements -> return_stmt, Semicolon
#line 139 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-2].node;
				}
#line default
        break;
      case 27: // statements -> block
#line 143 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 28: // if_stmt -> If, OpenPar, expr, ClosePar, statements, Else, statements
#line 149 "kompilator.y"
    {
				Compiler.IfElseOp n = new Compiler.IfElseOp(ValueStack[ValueStack.Depth-5].node, ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 29: // if_stmt -> If, OpenPar, expr, ClosePar, statements
#line 154 "kompilator.y"
    {
				Compiler.IfOp n = new Compiler.IfOp(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 30: // while_stmt -> While, OpenPar, expr, ClosePar, statements
#line 163 "kompilator.y"
    {
				Compiler.WhileOp n = new Compiler.WhileOp(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 31: // read_stmt -> Read, Ident
#line 170 "kompilator.y"
    {
				Compiler.ReadOp n = new Compiler.ReadOp(ValueStack[ValueStack.Depth-1].val, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 32: // write_stmt -> Write, expr
#line 177 "kompilator.y"
    {
				Compiler.WriteExprOp n = new Compiler.WriteExprOp(ValueStack[ValueStack.Depth-1].node, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 33: // write_stmt -> Write, Str
#line 182 "kompilator.y"
    {
				Compiler.WriteStrOp n = new Compiler.WriteStrOp(ValueStack[ValueStack.Depth-1].val, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 34: // return_stmt -> Return
#line 189 "kompilator.y"
    {
				Compiler.ReturnOp n = new Compiler.ReturnOp(Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 35: // expr_instruction -> expr, Semicolon
#line 196 "kompilator.y"
      {
						Compiler.SingleExprInstruction n = new Compiler.SingleExprInstruction(ValueStack[ValueStack.Depth-2].node, Compiler.lineno);
						CurrentSemanticValue.node = n;
						}
#line default
        break;
      case 36: // expr -> Ident, Assign, expr
#line 204 "kompilator.y"
    {
				Compiler.AssignOp n = new Compiler.AssignOp(ValueStack[ValueStack.Depth-1].node, ValueStack[ValueStack.Depth-3].val, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 37: // expr -> exprLogic
#line 209 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 38: // exprLogic -> exprLogic, LogicOr, exprRelation
#line 215 "kompilator.y"
    {
				Compiler.BinaryOpLogical n = new Compiler.BinaryOpLogical(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.LogicOr, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 39: // exprLogic -> exprLogic, LogicAnd, exprRelation
#line 220 "kompilator.y"
    {
				Compiler.BinaryOpLogical n = new Compiler.BinaryOpLogical(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.LogicAnd, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 40: // exprLogic -> exprRelation
#line 225 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 41: // exprRelation -> exprRelation, Equal, exprAdd
#line 231 "kompilator.y"
    {
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.Equal, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 42: // exprRelation -> exprRelation, NotEqual, exprAdd
#line 236 "kompilator.y"
    {
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.NotEqual, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 43: // exprRelation -> exprRelation, More, exprAdd
#line 241 "kompilator.y"
    {
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.More, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 44: // exprRelation -> exprRelation, MoreEqual, exprAdd
#line 246 "kompilator.y"
    {
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.MoreEqual, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 45: // exprRelation -> exprRelation, Less, exprAdd
#line 251 "kompilator.y"
    {
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.Less, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 46: // exprRelation -> exprRelation, LessEqual, exprAdd
#line 256 "kompilator.y"
    {
				Compiler.BinaryOpRelation n = new Compiler.BinaryOpRelation(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.LessEqual, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 47: // exprRelation -> exprAdd
#line 261 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 48: // exprAdd -> exprAdd, Plus, exprMul
#line 267 "kompilator.y"
    {
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.Plus, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 49: // exprAdd -> exprAdd, Minus, exprMul
#line 272 "kompilator.y"
    {
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.Minus, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 50: // exprAdd -> exprMul
#line 277 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 51: // exprMul -> exprMul, Multiplies, exprBit
#line 283 "kompilator.y"
    {
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.Multiplies, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 52: // exprMul -> exprMul, Divides, exprBit
#line 288 "kompilator.y"
    {
				Compiler.BinaryOpAddMul n = new Compiler.BinaryOpAddMul(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.Divides, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 53: // exprMul -> exprBit
#line 293 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 54: // exprBit -> exprBit, BitOr, exprUn
#line 299 "kompilator.y"
    {
				Compiler.BinaryOpBitwise n = new Compiler.BinaryOpBitwise(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.BitOr, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 55: // exprBit -> exprBit, BitAnd, exprUn
#line 304 "kompilator.y"
    {
				Compiler.BinaryOpBitwise n = new Compiler.BinaryOpBitwise(ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node, Tokens.BitAnd, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 56: // exprBit -> exprUn
#line 309 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 57: // exprUn -> Minus, exprUn
#line 315 "kompilator.y"
    {
				Compiler.UnaryOp n = new Compiler.UnaryOp(ValueStack[ValueStack.Depth-1].node, Tokens.Minus, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 58: // exprUn -> BitNeg, exprUn
#line 320 "kompilator.y"
    {
				Compiler.UnaryOp n = new Compiler.UnaryOp(ValueStack[ValueStack.Depth-1].node, Tokens.BitNeg, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 59: // exprUn -> LogicNeg, exprUn
#line 325 "kompilator.y"
    {
				Compiler.UnaryOp n = new Compiler.UnaryOp(ValueStack[ValueStack.Depth-1].node, Tokens.LogicNeg, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 60: // exprUn -> OpenPar, Int, ClosePar, exprUn
#line 330 "kompilator.y"
    {
				Compiler.UnaryOp n = new Compiler.UnaryOp(ValueStack[ValueStack.Depth-1].node, Tokens.Int, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 61: // exprUn -> OpenPar, Double, ClosePar, exprUn
#line 335 "kompilator.y"
    {
				Compiler.UnaryOp n = new Compiler.UnaryOp(ValueStack[ValueStack.Depth-1].node, Tokens.Double, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 62: // exprUn -> OpenPar, expr, ClosePar
#line 340 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-2].node;
				}
#line default
        break;
      case 63: // exprUn -> value
#line 344 "kompilator.y"
    {
				CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node;
				}
#line default
        break;
      case 64: // value -> IntNumber
#line 350 "kompilator.y"
    {
				Compiler.IntNumber n = new Compiler.IntNumber(int.Parse(ValueStack[ValueStack.Depth-1].val));
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 65: // value -> DoubleNumber
#line 355 "kompilator.y"
    {
				double d = double.Parse(ValueStack[ValueStack.Depth-1].val,System.Globalization.CultureInfo.InvariantCulture);
				Compiler.DoubleNumber n = new Compiler.DoubleNumber(d);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 66: // value -> True
#line 361 "kompilator.y"
    {
				Compiler.BoolNumber n = new Compiler.BoolNumber(true);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 67: // value -> False
#line 366 "kompilator.y"
    {
				Compiler.BoolNumber n = new Compiler.BoolNumber(false);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
      case 68: // value -> Ident
#line 371 "kompilator.y"
    {
				Compiler.Ident n = new Compiler.Ident(ValueStack[ValueStack.Depth-1].val, Compiler.lineno);
				CurrentSemanticValue.node = n;
				}
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 378 "kompilator.y"

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
