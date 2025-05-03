using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;


public class Declaracion:Instruccion{

    public string Nombre;
    public Tipodato? Tipo;

    public Expresion? Valor;


    public Declaracion(int linea, int columna, string nombre, Tipodato tipo, Expresion valor):base(linea, columna, TipoI.DECLARACION){

        Nombre = nombre;
        Tipo = tipo;
        Valor = valor;


    }

 public Declaracion(int linea, int columna, string nombre, Tipodato tipo):base(linea, columna, TipoI.DECLARACION ){

        Nombre = nombre;
        Tipo = tipo;
       
    }


 public Declaracion(int linea, int columna, string nombre, Expresion valor):base(linea, columna, TipoI.DECLARACION ){

        Nombre = nombre;
        Valor = valor;

    }


    public override TipoRetorno? Interpretar(GenARM gen)
    {

             return null; 

    }


}