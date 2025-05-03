using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;


public class Llamadafunc:Expresion{

    private string Nombre;
    private List<Expresion> Argumentos;

    public Llamadafunc(int linea, int columna, string nombre, List<Expresion> argumentos):
    base(linea, columna, TipoE.LLAMADAFUNC){

        Nombre = nombre;
        Argumentos = argumentos;
        
    }

    public override TipoRetorno? Interpretar(GenARM gen){  //e entorno que llega

    return null;

    }


}