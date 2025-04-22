using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


public class While:Instruccion{

private Expresion Condicion;
private Instruccion Bloque;



public While(int linea, int columna, Expresion condicion, Instruccion bloque)
:base(linea, columna, TipoI.WHILE){

    Condicion= condicion;
    Bloque = bloque;

}

    public override TipoRetorno? Interpretar(Entorno e)
    {

        Entorno local = new (e, e.Nombre);
        TipoRetorno condicion = Condicion.Interpretar(local);

            while(condicion.Valor.Equals ("true")){
                TipoRetorno bloque = Bloque.Interpretar(local);
                if(bloque!= null){
                    if(bloque.Valor.Equals (TipoI.CONTINUE)){
                         condicion = Condicion.Interpretar(local);
                         continue;
                    }
                    if(bloque.Valor.Equals (TipoI.BREAK)){
                         break;
                    }
                    return bloque;
                }
                 condicion = Condicion.Interpretar(local);
            }
            return null;
    }
}