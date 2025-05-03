namespace OLC2_Proyecto2_201612218.src.Backend.parser;
using Antlr4.Runtime;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class LexerErrorListener : IAntlrErrorListener<int> {
    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
        //Console.WriteLine($"Caracter no reconocido: \"{msg}\". {line}:{charPositionInLine}");
        Consola.Errores.Add(new Error(line,charPositionInLine,TipoError.LEXICO, msg));

    }
}