
%using QUT.Gppg;
%namespace GardensPoint



IntNumber   0|[1-9][0-9]*
DoubleNumber  (0|[1-9][0-9]*)\.[0-9]+
Ident       [a-zA-z][a-zA-Z0-9]*
Comment		\/\/(.)*\n
Str			\"(\\.|[^\\\"\n])*\"

%% 

"program"		{ return (int)Tokens.Program; }
"if"			{ return (int)Tokens.If; }
"else"			{ return (int)Tokens.Else; }
"while" 		{ return (int)Tokens.While; }
"read" 			{ return (int)Tokens.Read; }
"write"			{ return (int)Tokens.Write; }
"return"		{ return (int)Tokens.Return; }
"int"			{ return (int)Tokens.Int; }
"double"		{ return (int)Tokens.Double; }
"bool"			{ return (int)Tokens.Bool; }
"true"			{ return (int)Tokens.True; }
"false" 		{ return (int)Tokens.False; }			
{IntNumber}   	{ yylval.val=yytext; return (int)Tokens.IntNumber; }
{DoubleNumber}  { yylval.val=yytext; return (int)Tokens.DoubleNumber; }
{Ident}       	{ yylval.val=yytext; return (int)Tokens.Ident; }
{Str}			{ yylval.val=yytext; return (int)Tokens.Str; }
{Comment}		{ Compiler.lineno++; }
"="           	{ return (int)Tokens.Assign; }
"+"           	{ return (int)Tokens.Plus; }
"-"           	{ return (int)Tokens.Minus; }
"*"           	{ return (int)Tokens.Multiplies; }
"/"           	{ return (int)Tokens.Divides; }
"("           	{ return (int)Tokens.OpenPar; }
")"           	{ return (int)Tokens.ClosePar; }
"||"			{ return (int)Tokens.LogicOr; }
"&&"			{ return (int)Tokens.LogicAnd; }
"|"				{ return (int)Tokens.BitOr; }
"&"				{ return (int)Tokens.BitAnd; }
"=="			{ return (int)Tokens.Equal; }
"!="			{ return (int)Tokens.NotEqual; }
">"				{ return (int)Tokens.More; }
">="			{ return (int)Tokens.MoreEqual; }
"<"				{ return (int)Tokens.Less; }
"<="			{ return (int)Tokens.LessEqual; }
"!"				{ return (int)Tokens.LogicNeg; }
"~"				{ return (int)Tokens.BitNeg; }
"{" 			{ return (int)Tokens.OpenBr; }
"}"				{ return (int)Tokens.CloseBr; }
";"				{ return (int)Tokens.Semicolon; }
"\r"          	{ }
"\n"			{ Compiler.lineno++; }
"\r\n"			{ Compiler.lineno++; }
<<EOF>>       	{ return (int)Tokens.Eof; }
" "           	{ }
"\t"          	{ }
.             	{ return (int)Tokens.Error; }