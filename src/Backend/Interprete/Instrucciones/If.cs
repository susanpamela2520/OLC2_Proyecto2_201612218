using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class If:Instruccion{

private Expresion Condicion;
private Instruccion Bloque;

private Instruccion? Else;


public If(int linea, int columna, Expresion condicion, Instruccion bloque, Instruccion? else_)
:base(linea, columna, TipoI.IF){

    Condicion= condicion;
    Bloque = bloque;
    Else = else_;
}

    public override TipoRetorno? Interpretar(Entorno e)
    {
       }

}