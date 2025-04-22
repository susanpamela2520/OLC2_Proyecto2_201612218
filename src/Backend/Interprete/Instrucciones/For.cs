using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;


public class For:Instruccion{

private Instruccion Inicializacion;
private Expresion Condicion;
private Instruccion Actualizacion;

private Instruccion Bloque; 

public For (int linea, int columna, Instruccion inicializacion, Expresion condicion, Instruccion actualizacion , Instruccion bloque):
    
    base(linea, columna, TipoI.FOR){

       Inicializacion= inicializacion;
       Condicion = condicion;
       Actualizacion = actualizacion;
       Bloque = bloque;

    }


    public override TipoRetorno? Interpretar(Entorno e)
    {

        Entorno local = new (e, e.Nombre);
        Inicializacion.Interpretar(local); //se inicializa la variable de iteracion.

        TipoRetorno condicion = Condicion.Interpretar(local);

        while(condicion.Valor.Equals ("true")){
                TipoRetorno bloque = Bloque.Interpretar(local);
                if(bloque!= null){
                    if(bloque.Valor.Equals (TipoI.CONTINUE)){
                         Actualizacion.Interpretar(local);
                         condicion = Condicion.Interpretar(local);
                         continue;
                    }
                    if(bloque.Valor.Equals (TipoI.BREAK)){
                         break;
                    }
                    return bloque;
                }

            Actualizacion.Interpretar(local);
            condicion = Condicion.Interpretar(local);
        }

        return null;
    }

}

  