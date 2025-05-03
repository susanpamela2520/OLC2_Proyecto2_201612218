using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

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

    public override TipoRetorno? Interpretar(GenARM gen)
    {

        return null;

       }

}