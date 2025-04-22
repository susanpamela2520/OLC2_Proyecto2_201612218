using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


public class ForEach:Instruccion{

private string Indice;
private string Valor;

private Expresion Slice; 
private Instruccion Bloque; 

public ForEach (int linea, int columna, string indice, string valor,  Expresion slice, Instruccion bloque):
    
    base(linea, columna, TipoI.FOR){

       Indice = indice;
       Valor = valor;
       Slice = slice; 
       Bloque = bloque;
    }


    public override TipoRetorno? Interpretar(Entorno e)
    {

       
    }

}

  