namespace OLC2_Proyecto2_201612218.src.Backend.parser;
using Antlr4.Runtime;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class SyntaxErrorListener : BaseErrorListener {
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
       // Console.WriteLine($"Error de sintaxis: \"{msg}\". {line}:{charPositionInLine}");

       Consola.Errores.Add(new Error(line,charPositionInLine,TipoError.SINTACTICO, msg));
    }



}