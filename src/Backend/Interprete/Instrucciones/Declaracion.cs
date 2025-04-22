using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


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


    public override TipoRetorno? Interpretar(Entorno e)
    {

              

    }


}