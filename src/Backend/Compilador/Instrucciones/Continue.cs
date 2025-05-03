using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

public class Continue:Instruccion{

  
    public Continue(int linea, int columna):
    base(linea, columna, TipoI.CONTINUE){

          }

    public override TipoRetorno Interpretar(GenARM gen)
    {
      
     return null;
    }

}