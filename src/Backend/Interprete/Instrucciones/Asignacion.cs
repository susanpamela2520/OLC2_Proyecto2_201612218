using Microsoft.Extensions.Diagnostics.HealthChecks;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

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

    public override TipoRetorno? Interpretar(Entorno e, GenARM gen)
    {
      
       return null;

    }

}


 