using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Slice:Expresion{

private List<Expresion> Elementos;
private Tipo? Secundario;

public Slice(int linea, int columna, List<Expresion> elementos):
    base(linea, columna, TipoE.VECTOR){

        Elementos = elementos;

    }

    public override TipoRetorno Interpretar(GenARM gen)
    {
       
       return null;

    }


private List<TipoRetorno> ConstruirSlice(GenARM gen){
    
    return null;

}

}