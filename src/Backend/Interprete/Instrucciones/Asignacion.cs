using Microsoft.Extensions.Diagnostics.HealthChecks;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;

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

    public override TipoRetorno? Interpretar(Entorno e)
    {
      
        TipoRetorno valor = Valor.Interpretar(e);
        if(Indices == null ){
             e.ActualizarVariable(Variable,valor, Linea, Columna);
        }else{

            List <int []> indices = new ();

            TipoRetorno indice;
            foreach(Expresion i in Indices){
                indice = i.Interpretar(e);
                if(indice.Tipobase != Tipo.INT){

                    e.GuardarError ($"Los indices solo pueden ser de tipo Int ", i.Linea, i.Columna);
                    return new TipoRetorno("nil", Tipo.NIL);
                }

                    indices.Add(new []{int.Parse(indice.Valor.ToString()), i.Linea, i.Columna});
            }
             e.ActualizarVariable(Variable, indices, valor, Linea, Columna);

        }
       
        return null;

    }

}


 