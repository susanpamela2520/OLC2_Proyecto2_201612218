using Microsoft.Extensions.Diagnostics.HealthChecks;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

public class Asignacion:Instruccion{

 private string Variable;
 private Expresion Valor;

 private List <Expresion>? Indices;
 

public Asignacion (int linea, int columna, string variable, Expresion valor)
:base(linea, columna, TipoI.ASIGNACION){

Variable= variable;
Valor = valor; 
}

//sobrecarga para reasignar posiciones de slices
public Asignacion (int linea, int columna, string variable, Expresion valor, List<Expresion> indices)
:base(linea, columna, TipoI.ASIGNACION){

Variable= variable;
Valor = valor; 
Indices = indices;

}

    public override TipoRetorno? Interpretar(GenARM gen)
    {
      
       return null;

    }

}


 