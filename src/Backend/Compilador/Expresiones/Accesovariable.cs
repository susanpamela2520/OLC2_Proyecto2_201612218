using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;


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

    public override TipoRetorno Interpretar(GenARM gen)
    {
        return null;
           
    }





}