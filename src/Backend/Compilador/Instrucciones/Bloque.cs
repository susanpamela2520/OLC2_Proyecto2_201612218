namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

//se encierra en llaves para crear los entornos hijos
public class Bloque : Instruccion {
    public List<Statement> Instrucciones;

    public Bloque(int linea, int columna, List<Statement> instrucciones) :
    base(linea, columna, TipoI.BLOQUE) {
        Instrucciones = instrucciones;
    }

    public override TipoRetorno? Interpretar(GenARM gen) {
        gen.AddComentario("========== Bloque ===========");
        gen.NuevoEntorno();

        foreach(Statement stmt in Instrucciones) {
            stmt.Interpretar(gen);
        }

        var bytesOffset = gen.TerminarEntorno();
        if(bytesOffset > 0) {
            gen.AddComentario("---------- Removiendo Bytes de la Pila ----------");
            gen.Mov(R.x0, bytesOffset);
            gen.Add(R.sp, R.sp, R.x0);
            gen.AddComentario("------------ Stack Pointer Ajustado -------------");
        }

        gen.AddComentario("======== Fin Bloque =========");
        return null;
    }
}