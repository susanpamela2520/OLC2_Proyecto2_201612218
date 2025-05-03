namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using System.Globalization;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

public class Primitivo : Expresion {
    public object Valor;
    public Tipo Tipo;

    public Primitivo (int linea, int columna, object valor, Tipo tipo) : 
    base(linea, columna, TipoE.PRIMITIVO) {
        Valor = valor;
        Tipo = tipo;
    }

    public override TipoRetorno Interpretar(Entorno e, GenARM gen) {
        gen.AddComentario("--------- Primitivo ---------");
        switch (Tipo) {
            case Tipo.INT:
                gen.Mov(R.x0, $"#{Valor}");
                gen.Push(R.x0);
                break;
            case Tipo.FLOAT:
                long floatBits = BitConverter.DoubleToInt64Bits(double.Parse(Valor.ToString()??"0.0", CultureInfo.InvariantCulture));
                short[] floatParts = new short[4];
                for(int i = 0; i < 4; i ++) {
                    floatParts[i] = (short) ((floatBits >> (i * 16)) & 0xFFFF);
                }

                gen.Movz(R.x0, $"#{floatParts[0]}", "#0");

                for(int i = 1; i < 4; i ++) {
                    gen.Movk(R.x0, $"#{floatParts[i]}", $"#{i * 16}");
                }

                gen.Push(R.x0);
                break;
            case Tipo.BOOL:
                gen.Mov(R.x0, Valor.Equals("true") ? "#1" : "#0");
                gen.Push(R.x0);
                break;
            case Tipo.RUNE:
                gen.Mov(R.x0, $"#{(int) Valor.ToString().ToCharArray()[0]}");
                gen.Push(R.x0);
                break;
            case Tipo.STRING:
                foreach(char c in Valor.ToString()) {
                    gen.Mov(R.x0, $"#{(int) c}");
                }
                break;
            default:
                break;
        };
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