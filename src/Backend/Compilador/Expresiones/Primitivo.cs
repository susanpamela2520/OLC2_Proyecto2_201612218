namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using System.Globalization;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class Primitivo : Expresion {
    public object Valor;
    public Tipo Tipo;

    public Primitivo (int linea, int columna, object valor, Tipo tipo) : 
    base(linea, columna, TipoE.PRIMITIVO) {
        Valor = valor;
        Tipo = tipo;
    }

    public override TipoRetorno Interpretar(GenARM gen) {
        gen.AddComentario("--------- Primitivo ---------");
        StackObject? objStack;
        switch (Tipo) {
            case Tipo.INT:
                objStack = gen.ObjetoInt();
                gen.AddComentario("--------- Entero ---------");
                gen.Mov(R.x0, int.Parse (Valor.ToString()));
                gen.Push(R.x0);
                gen.AddComentario("------- Fin Entero -------");
                break;
            case Tipo.FLOAT:
                gen.AddComentario("-------- Flotante --------");
                objStack = gen.ObjetoFloat();
                long floatBits = BitConverter.DoubleToInt64Bits(double.Parse(Valor.ToString()??"0.0", CultureInfo.InvariantCulture));
                short[] floatParts = new short[4];
                for(int i = 0; i < 4; i ++) {
                    floatParts[i] = (short) ((floatBits >> (i * 16)) & 0xFFFF);
                }

                gen.Movz(R.x0, $"#{floatParts[0]}", "#0");

                for(int i = 1; i < 4; i ++) {
                    gen.Movk(R.x0, $"{floatParts[i]}", $"{i * 16}");
                }

                gen.Push(R.x0);
                gen.AddComentario("------ Fin Flotante ------");
                break;
            case Tipo.BOOL:
                gen.AddComentario("-------- Booleano --------");
                objStack = gen.ObjetoBool();
                gen.Mov(R.x0, Valor.Equals("true") ? 1 : 0);
                gen.Push(R.x0);
                gen.AddComentario("------ Fin Booleano ------");
                break;
            case Tipo.RUNE:
                gen.AddComentario("---------- Rune ----------");
                objStack = gen.ObjetoRune();
                gen.Mov(R.x0, (int) Valor.ToString().ToCharArray()[0]);
                gen.Push(R.x0);
                gen.AddComentario("-------- Fin Rune --------");
                break;
            default:
                gen.AddComentario("--------- Cadena ---------");
                objStack = gen.ObjetoString();
                foreach(char c in Valor.ToString() ?? "") {
                    gen.Mov(R.x0, (int) c);
                }
                gen.AddComentario("------- Fin Cadena -------");
                break;
        };
        gen.PushObjeto(objStack);
        gen.AddComentario("------- Fin Primitivo -------");
        return null;
    }

    public string? FormatearValor() { 
        return Valor.ToString()? 
            .Replace("\\n", "\n")
            .Replace("\\t", "\t")
            .Replace("\\\"", "\"")
            .Replace("\\'", "\'")
            .Replace("\\\\", "\\");
    }
} 