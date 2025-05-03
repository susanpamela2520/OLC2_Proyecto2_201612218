using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Return:Expresion{

    private Expresion Expresion;


    public Return(int linea, int columna, Expresion expresion):
    base(linea, columna, TipoE.RETORNO){

        Expresion = expresion;

    }

    public override TipoRetorno Interpretar(GenARM gen)
    {
      
        return null;
}
}