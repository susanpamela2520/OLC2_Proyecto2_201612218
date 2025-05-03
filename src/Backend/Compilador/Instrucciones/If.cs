using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

public class If:Instruccion{

private Expresion Condicion;
private Instruccion Bloque;

private Instruccion? Else;


public If(int linea, int columna, Expresion condicion, Instruccion bloque, Instruccion? else_)
:base(linea, columna, TipoI.IF){

    Condicion= condicion;
    Bloque = bloque;
    Else = else_;
}

    public override TipoRetorno? Interpretar(GenARM gen)
    {
        gen.AddComentario("========== Instruccion If ===========");

        Condicion.Interpretar(gen);
        gen.PopObjeto(R.x0);

        if(Else != null){
        
        var etiquetaFalsa = gen.GetEtiqueta();
        var etiquetaFin = gen.GetEtiqueta();
        gen.Cbz (R.x0, etiquetaFalsa);
        Bloque.Interpretar(gen);
        gen.B(etiquetaFin);
        gen.AddEtiqueta(etiquetaFalsa);
        Else.Interpretar(gen);

        gen.AddEtiqueta(etiquetaFin);

            
        }else{
        var etiquetaFin = gen.GetEtiqueta();
        gen.Cbz (R.x0, etiquetaFin);
        Bloque.Interpretar(gen);
       

        gen.AddEtiqueta(etiquetaFin);


        }

        gen.AddComentario("========== Fin If ===========");
        return null;

       }

}