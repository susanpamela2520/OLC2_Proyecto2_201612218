using System.Globalization;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;

public class Primitivo:Expresion{

    public object Valor;
    public Tipo Tipo;
    public Primitivo (int linea, int columna, object valor, Tipo tipo): 
    base(linea, columna, TipoE.PRIMITIVO){
        Valor = valor;
        Tipo = tipo;
    }

    public override TipoRetorno Interpretar(Entorno e)
    {
       //Console.WriteLine(double.Parse(Valor.ToString()??"0.0", CultureInfo.InvariantCulture));
       return Tipo switch {
        Tipo.INT => new TipoRetorno (int.Parse(Valor.ToString()??"0"), Tipo),
        Tipo.FLOAT => new TipoRetorno (double.Parse(Valor.ToString()??"0.0", CultureInfo.InvariantCulture),Tipo),
        Tipo.BOOL => new TipoRetorno (Valor.Equals("true") ? "true" : "false",Tipo),
        Tipo.RUNE => new TipoRetorno (FormatearValor()??"\0",Tipo),
        Tipo.STRING => new TipoRetorno (FormatearValor()??"",Tipo),
        _ => new TipoRetorno ("nil",Tipo),


       };

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