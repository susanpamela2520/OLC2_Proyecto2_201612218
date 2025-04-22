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
       
    }


private List<TipoRetorno> ConstruirSlice(Entorno e, List<Expresion> elementos){
    
    
}

}