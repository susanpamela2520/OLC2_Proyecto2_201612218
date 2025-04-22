cd src/Backend/parser
java -jar antlr-4.13.2-complete.jar -Dlanguage=CSharp *.g4
sed -i 's/.CharPositionInLine:0/.Column:0/g' ./ParserParser.cs