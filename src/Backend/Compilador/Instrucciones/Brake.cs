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
        if(gen.Frame == null) {
            gen.AddComentario("Sentencia Break");

            if(gen.EtiquetaBreak != null){
                gen.B(gen.EtiquetaBreak);
            }
            gen.AddComentario("Fin Sentencia Break");
        }

        return null;
    }

}