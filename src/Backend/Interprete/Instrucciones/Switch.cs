using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class Switch:Instruccion{

    private Expresion Argumento;
     public List<Case> Cases;


     public Switch(int linea, int columna, Expresion argumento, List<Case> cases)
     :base(linea, columna, TipoI.SWITCH){

        Argumento=argumento;
        Cases = cases;

     }

    public override TipoRetorno? Interpretar(Entorno e)
    {
       
    }

}