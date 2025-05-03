using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;


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


    public override TipoRetorno? Interpretar(GenARM gen)
    {
         return null;
       
    }

}

  