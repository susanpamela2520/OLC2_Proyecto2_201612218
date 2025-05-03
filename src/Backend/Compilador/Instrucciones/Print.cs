namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class Print : Instruccion {
    public List <Expresion> Expresiones;

    public Print(int linea, int columna, List <Expresion> expresiones) :
    base (linea, columna, TipoI.PRINT) {
        Expresiones = expresiones;
    }

    public override TipoRetorno? Interpretar(GenARM gen) {
        gen.AddComentario("=========== Print ===========");
        var espacio = false;
        foreach(Expresion exp in Expresiones) {
            if(espacio){
                gen.ImprimirEspacio();
            }
            exp.Interpretar(gen); // expresion: 1 + 2 = 3 // Stack: | 3 |
            var valorFloat = gen.TopePila().Type == Tipo.FLOAT;
            var valor = gen.PopObjeto(valorFloat? R.d0:R.x0);
            if(valor.Type == Tipo.INT) {
                gen.ImprimirInt(R.x0);
            }else if(valor.Type == Tipo.STRING){
                gen.ImprimirString(R.x0);
            }else if(valor.Type == Tipo.FLOAT){
                gen.ImprimirFloat();
            }
            espacio = true;
            
        }
        gen.ImprimirSalto();
        gen.AddComentario("========= Fin Print =========");
        return null;
    }


}