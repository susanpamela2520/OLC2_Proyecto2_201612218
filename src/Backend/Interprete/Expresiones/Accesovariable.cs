using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;


public class Accesovariable:Expresion{

    public string Nombre;
    public List <Expresion>? Indices;



public Accesovariable(int linea, int columna, string nombre):base(linea, columna, TipoE.ACCESOVAR){
    
    Nombre = nombre;

}

public Accesovariable(int linea, int columna, string nombre, List < Expresion > indices):base(linea, columna, TipoE.ACCESOVAR){
    
    Nombre = nombre;
    Indices = indices;
}

    public override TipoRetorno Interpretar(Entorno e)
    {

            Variable? variable = null;

        if(Indices == null){
            variable = e.ObtenerVariable(Nombre,Linea, Columna);
        }else{
            //acceder a posiciones de Slice   
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
            variable = e.ObtenerVariable(Nombre, indices, Linea, Columna);

        }

        if(variable != null){
            return variable.Valor;
        }

        return new TipoRetorno("nil", Tipo.NIL);
        

    }





}