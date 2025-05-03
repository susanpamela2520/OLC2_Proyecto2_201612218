using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class Funcion: Instruccion {

    public string Nombre;
    public List < Parametro > Parametros;
    
    public Instruccion BloqueIntrucciones;

    public Tipodato Tiporetorno;


    public Funcion (int linea, int columna, string nombre, List < Parametro > parametros, Instruccion instrucciones, Tipodato tipo):

    base (linea, columna, TipoI.FUNCION){

        Nombre = nombre;
        
        Parametros = parametros;
        BloqueIntrucciones = instrucciones;
        Tiporetorno = tipo;
        
    }


    public override TipoRetorno? Interpretar(Entorno e)
    {
        e.Guardarfuncion (this);
        if (Nombre.Equals("main")){
            //llamada a funcion

            new Llamadafunc (0,0 , Nombre, new List<Expresion>()).Interpretar(e);

        }
            return null;

    }



}