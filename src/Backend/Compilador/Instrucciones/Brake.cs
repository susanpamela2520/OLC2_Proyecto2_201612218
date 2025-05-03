using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

public class Break:Instruccion{

  
    public Break(int linea, int columna):
    base(linea, columna, TipoI.BREAK){

          }

    public override TipoRetorno Interpretar(GenARM gen)
    {
      return null;
     
    }

}