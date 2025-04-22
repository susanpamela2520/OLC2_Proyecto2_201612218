using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


public class For:Instruccion{

private Instruccion Inicializacion;
private Expresion Condicion;
private Instruccion Actualizacion;

private Instruccion Bloque; 

public For (int linea, int columna, Instruccion inicializacion, Expresion condicion, Instruccion actualizacion , Instruccion bloque):
    
    base(linea, columna, TipoI.FOR){

       Inicializacion= inicializacion;
       Condicion = condicion;
       Actualizacion = actualizacion;
       Bloque = bloque;

    }


    public override TipoRetorno? Interpretar(Entorno e)
    {

        
    }

}

  