using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;


public class While:Instruccion{

private Expresion Condicion;
private Instruccion Bloque;



public While(int linea, int columna, Expresion condicion, Instruccion bloque)
:base(linea, columna, TipoI.WHILE){

    Condicion= condicion;
    Bloque = bloque;

}

    public override TipoRetorno? Interpretar(GenARM gen)
    {
      
        return null;

    }
}