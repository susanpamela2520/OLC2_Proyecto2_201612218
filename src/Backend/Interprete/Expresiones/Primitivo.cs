using System.Globalization;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

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
       
    }

    public string? FormatearValor() { 
       
    }
} 