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

           
    }





}