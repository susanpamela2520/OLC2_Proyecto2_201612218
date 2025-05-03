namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

public class Entorno{
    private Entorno? Padre;
    public string Nombre;
    public int Profundidad = 0;
    public Stack<Valorstack> Stack = new();
    public string? EtiquetaContinue = null;
    public string? EtiquetaReturn = null;
    public string? EtiquetaBreak = null;
    public Dictionary<string, MetaData> Funciones = new();
    public bool EstaEnFuncion = false;
    public int IndiceDeclaracion = 0;
    public List<Valorstack> Frame = new();
    public int LocalSize = 0;
    public int Offset = 0;

    public Entorno(Entorno? padre, string nombre) {
        Padre = padre;
        Nombre = nombre;
    }

    public Entorno(Entorno? padre, string nombre, int offset) {
        Padre = padre;
        Nombre = nombre;
        Offset = offset;
    }

    public void GuardarError(string descripcion, int linea, int columna) { //insertartando errores SEMANTICOS
        Consola.Errores.Add(new Error(linea, columna, TipoError.SEMANTICO, descripcion));
    }

    public void EnviarImpresion (string cadena){
        Consola.Impresiones.Add(cadena);
    }
}