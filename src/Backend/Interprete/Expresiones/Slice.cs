using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

public class Slice:Expresion{

private List<Expresion> Elementos;
private Tipo? Secundario;

public Slice(int linea, int columna, List<Expresion> elementos):
    base(linea, columna, TipoE.VECTOR){

        Elementos = elementos;

    }

    public override TipoRetorno Interpretar(Entorno e)
    {
        List<TipoRetorno> Slice = ConstruirSlice(e, Elementos);
        return new TipoRetorno (Slice, Tipo.SLICE, Secundario, Slice.Count > 0? Slice[0].Dimensiones+1:1);
    }


private List<TipoRetorno> ConstruirSlice(Entorno e, List<Expresion> elementos){
    
    List<TipoRetorno> nuevoSlice = new ();
    TipoRetorno elemento;

    foreach(Expresion el in Elementos){

        elemento = el.Interpretar(e);
            if(Secundario == null  ){
                if(elemento.Tipobase != Tipo.SLICE){
                    Secundario = elemento.Tipobase;
                }else{
                    Secundario = elemento.Tiposecundario;
                }

        }
        nuevoSlice.Add(elemento);
    }
    return nuevoSlice;
}

}