using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

public class Return:Expresion{

    private Expresion Expresion;


    public Return(int linea, int columna, Expresion expresion):
    base(linea, columna, TipoE.RETORNO){

        Expresion = expresion;

    }

    public override TipoRetorno Interpretar(Entorno e)
    {
      

}
}