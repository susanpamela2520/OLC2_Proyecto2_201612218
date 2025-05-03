using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


public class Case:Instruccion{

    private Expresion? Valor;
    private Instruccion Bloque;
    private TipoRetorno? ValorArgumento;


public Case(int linea, int columna, Expresion? valor, Instruccion bloque):base(linea, columna, TipoI.CASE){

Valor=valor;
Bloque=bloque;
}

public void EnviarArgumento(TipoRetorno valorArgumento){

    ValorArgumento = valorArgumento;
}

    public override TipoRetorno? Interpretar(Entorno e)
    {
        Entorno local = new(e, e.Nombre);
        if(Valor!= null){
            TipoRetorno? valorCase = Valor.Interpretar(local);
            if(valorCase.Valor.Equals(ValorArgumento.Valor)){
                TipoRetorno? bloque = Bloque.Interpretar(local);
                if(bloque != null){
                    return bloque;
                }
                return new Break(0,0).Interpretar(local);
            }
        }else{            
            TipoRetorno? bloque = Bloque.Interpretar(local);
                    if(bloque != null){
                        return bloque;
                    }
        }
        return null;
    }

}