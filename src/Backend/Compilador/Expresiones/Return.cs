using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Return:Expresion{
    private Expresion Expresion;

    public Return(int linea, int columna, Expresion expresion) :
    base(linea, columna, TipoE.RETORNO) {

        Expresion = expresion;

    }

    public override TipoRetorno Interpretar(GenARM gen) {
        if(gen.Frame == null) {
            gen.AddComentario("Sentencia Return");

            if(Expresion == null){
                gen.B(gen.EtiquetaReturn);
            } else {
                if(gen.EstaEnFuncion == null) throw new Exception("Return está fuera de una función");
                
                Expresion.Interpretar(gen);
                gen.PopObjeto(R.x0);

                var tamanoFrame = gen.Funciones[gen.EstaEnFuncion].TamanoFrame;
                var offsetReturn = tamanoFrame - 1;
                gen.Mov(R.x1, offsetReturn * 8);
                gen.Sub(R.x1, R.x29, R.x1);
                gen.Str(R.x0, R.x1);
                gen.B(gen.EtiquetaReturn);
            }
            gen.AddComentario("Fin Sentencia Return");
        }

        return null;
    }
}