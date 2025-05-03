using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;


public class For:Instruccion{

private Instruccion Inicializacion;
private Expresion Condicion;
private Instruccion Actualizacion;

private Instruccion Bloque; 

public For (int linea, int columna, Instruccion inicializacion, Expresion condicion, Instruccion actualizacion , Instruccion bloque):
    
    base(linea, columna, TipoI.FOR){

       Inicializacion= inicializacion;
       Condicion = condicion;
       Actualizacion = actualizacion;
       Bloque = bloque;

    }

    public override TipoRetorno? Interpretar(GenARM gen)
    {
        if(gen.Frame == null) {
            gen.AddComentario("========== Instruccion For ===========");

            var etiquetaIncio = gen.GetEtiqueta();
            var etiquetaFin = gen.GetEtiqueta();
            var etiquetaIncremento = gen.GetEtiqueta();
            var etiquetaContinueAnterior = gen.EtiquetaContinue;
            var etiquetaBreakAnterior = gen.EtiquetaBreak;

            gen.EtiquetaContinue = etiquetaIncremento;
            gen.EtiquetaBreak = etiquetaFin;

            gen.NuevoEntorno();
            Inicializacion.Interpretar(gen);

            gen.AddEtiqueta(etiquetaIncio);

            Condicion.Interpretar(gen);
            gen.PopObjeto(R.x0);

            gen.Cbz(R.x0, etiquetaFin);

            Bloque.Interpretar(gen);
            gen.AddEtiqueta(etiquetaIncremento);
            Actualizacion.Interpretar(gen);

            gen.B(etiquetaIncio);
            gen.AddEtiqueta(etiquetaFin);

            var bytesOffset = gen.TerminarEntorno();
            if(bytesOffset > 0) {
                gen.AddComentario("---------- Removiendo Bytes de la Pila ----------");
                gen.Mov(R.x0, bytesOffset);
                gen.Add(R.sp, R.sp, R.x0);
                gen.AddComentario("------------ Stack Pointer Ajustado -------------");
            }

            gen.EtiquetaContinue = etiquetaContinueAnterior;
            gen.EtiquetaBreak = etiquetaBreakAnterior;
        
            gen.AddComentario("========== Fin For ===========");
        } else {
            Inicializacion.Interpretar(gen);
            Bloque.Interpretar(gen);
        }
        return null;
    }

}

  