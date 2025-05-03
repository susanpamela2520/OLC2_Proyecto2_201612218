namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class Funcion : Instruccion {
    public string Nombre;
    public List<Parametro> Parametros;
    public Instruccion BloqueIntrucciones;
    public Tipodato Tiporetorno;

    public Funcion(int linea, int columna, string nombre, List<Parametro> parametros, Instruccion instrucciones, Tipodato tipo) :
    base (linea, columna, TipoI.FUNCION) {
        Nombre = nombre;
        Parametros = parametros;
        BloqueIntrucciones = instrucciones;
        Tiporetorno = tipo;
    }

    public override TipoRetorno? Interpretar(GenARM gen) {
        gen.AddComentario("==== DECLARACION FUNCION ====");
        BloqueIntrucciones.Interpretar(gen);
        gen.AddComentario("== FIN DECLARACION FUNCION ==");
        return null;
    }
}