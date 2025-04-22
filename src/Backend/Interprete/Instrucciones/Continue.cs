using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;

public class Continue:Instruccion{

  
    public Continue(int linea, int columna):
    base(linea, columna, TipoI.CONTINUE){

          }

    public override TipoRetorno Interpretar(Entorno e)
    {
      
       return new TipoRetorno(Tipoi, Tipo.NIL);
    }

}