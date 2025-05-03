using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;


public class While:Instruccion{

private Expresion Condicion;
private Instruccion Bloque;



public While(int linea, int columna, Expresion condicion, Instruccion bloque)
:base(linea, columna, TipoI.WHILE){

    Condicion= condicion;
    Bloque = bloque;

}

    public override TipoRetorno? Interpretar(GenARM gen)
    {

        gen.AddComentario("========== Instruccion While ===========");

        var etiquetaIncio = gen.GetEtiqueta();
        var etiquetaFin = gen.GetEtiqueta();
        var etiquetaContinueAnterior = gen.EtiquetaContinue;
        var etiquetaBreakAnterior = gen.EtiquetaBreak;

        gen.EtiquetaContinue = etiquetaIncio;
        gen.EtiquetaBreak = etiquetaFin;
        gen.AddEtiqueta(etiquetaIncio);
        

        Condicion.Interpretar(gen);
        gen.PopObjeto(R.x0);

        gen.Cbz(R.x0, etiquetaFin);

        Bloque.Interpretar(gen);
        gen.B(etiquetaIncio);
        gen.AddEtiqueta(etiquetaFin);

        gen.EtiquetaContinue = etiquetaContinueAnterior;
        gen.EtiquetaBreak = etiquetaBreakAnterior;
    

        gen.AddComentario("========== Fin While ===========");
        return null;

    }
}