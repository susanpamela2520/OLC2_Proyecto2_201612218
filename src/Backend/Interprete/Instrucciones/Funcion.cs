using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
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


    public override TipoRetorno? Interpretar(Entorno e, GenARM gen)
    {
       gen.AddComentario("==== DECLARACION FUNCION ====");
        gen.AddEtiqueta(Nombre);
        if(Nombre.Equals("main")){
            gen.Mov(R.x0, "#93");
            gen.Svc("#0");
        }
        gen.AddComentario("== FIN DECLARACION FUNCION ==");
        return null;

    }



}