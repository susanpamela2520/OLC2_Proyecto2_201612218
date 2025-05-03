namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

//se encierra en llaves para crear los entornos hijos
public class Bloque : Instruccion {
    public List<Statement> Instrucciones;

    public Bloque(int linea, int columna, List<Statement> instrucciones) :
    base(linea, columna, TipoI.BLOQUE) {
        Instrucciones = instrucciones;
    }

    public override TipoRetorno? Interpretar(Entorno e, GenARM gen) {
        foreach(Statement stmt in Instrucciones) {
            stmt.Interpretar(e, gen);
        }
        return null;
    }
}