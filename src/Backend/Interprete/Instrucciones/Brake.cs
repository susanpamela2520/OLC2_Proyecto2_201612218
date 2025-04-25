using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class Break:Instruccion{

  
    public Break(int linea, int columna):
    base(linea, columna, TipoI.BREAK){

          }

    public override TipoRetorno Interpretar(Entorno e, GenARM gen)
    {
      return null;
     
    }

}