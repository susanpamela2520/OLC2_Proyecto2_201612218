namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class Parametro{

    public int Linea;
    public int Columna;
    public string Nombre;


    public Tipodato Tipo;

    public Parametro(int linea, int columna, string nombre, Tipodato tipo   ){

        Linea = linea;
        Columna = columna;
        Nombre = nombre;
        Tipo = tipo;

    }

}