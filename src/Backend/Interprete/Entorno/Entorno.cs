using System.Reflection.Metadata.Ecma335;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;


namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;

public class Entorno{
private Entorno? Padre;
public string Nombre;
public int Offset;
public Stack <Valorstack> Objectstack = new ();

public string? EtiquetaContinua = null;
public string? EtiquetaReturn = null;
public string? EtiquetaBreak = null;
public Dictionary <string, MetaData> Funciones = new ();
public bool EstaEnFuncion = false;
public int IndiceDeclaracion = 0;
public List <Valorstack> Frame = new ();
public int LocalSize = 0;



//Offset, desplazamiento en bites
//para manejar entornos y pilas.
public Entorno (Entorno? padre, string nombre, int offset){
Padre=padre;
Nombre = nombre;
Offset = offset;

}

//Sobrecarga Constructuor 
public Entorno (Entorno? padre, string nombre){
Padre=padre;
Nombre = nombre;

}

public void GuardarError(string descripcion, int linea, int columna){   //insertartando errores SEMNATICOS

    Consola.Errores.Add(new Error(linea, columna, TipoError.SEMANTICO, descripcion));

}

public void EnviarImpresion (string cadena){

        Consola.Impresiones.Add(cadena);
}




}