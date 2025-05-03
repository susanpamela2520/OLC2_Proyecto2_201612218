using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

public class Switch:Instruccion{

    private Expresion Argumento;
    public List<Case> Cases;


    public Switch(int linea, int columna, Expresion argumento, List<Case> cases) :
    base(linea, columna, TipoI.SWITCH){
        Argumento=argumento;
        Cases = cases;
    }

    public override TipoRetorno? Interpretar(GenARM gen) {
        if(gen.Frame == null) {
            
        }
        return null;
    }

}