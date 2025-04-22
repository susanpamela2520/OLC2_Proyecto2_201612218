using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;


public class Llamadafunc:Expresion{

    private string Nombre;
    private List<Expresion> Argumentos;

    public Llamadafunc(int linea, int columna, string nombre, List<Expresion> argumentos):
    base(linea, columna, TipoE.LLAMADAFUNC){

        Nombre = nombre;
        Argumentos = argumentos;
        
    }

    public override TipoRetorno? Interpretar (Entorno e){  //e entorno que llega

    
    }


}