using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class If:Instruccion{

private Expresion Condicion;
private Instruccion Bloque;

private Instruccion? Else;


public If(int linea, int columna, Expresion condicion, Instruccion bloque, Instruccion? else_)
:base(linea, columna, TipoI.IF){

    Condicion= condicion;
    Bloque = bloque;
    Else = else_;
}

    public override TipoRetorno? Interpretar(Entorno e)
    {
        Entorno local = new (e, e.Nombre);
        TipoRetorno condicion = Condicion.Interpretar(local);

            if(condicion.Valor.Equals ("true")){
                TipoRetorno bloque = Bloque.Interpretar(local);
                if(bloque!= null){
                    return bloque;
                }
                return null;
            }
            if(Else != null ){
                TipoRetorno else_ = Else.Interpretar(local);
                 if(else_!= null){
                    return else_;
                }
               
            }

        return null;
    }

}