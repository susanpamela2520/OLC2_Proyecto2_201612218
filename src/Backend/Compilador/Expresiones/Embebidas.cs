using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Embebidas:Expresion{

private string Nombre;
private Expresion Argumento;

private Expresion Argumento2;

public Embebidas(int linea, int columna, string nombre, Expresion argumento)
:base(linea, columna, TipoE.EMBEBIDAS){

    Nombre = nombre;
    Argumento = argumento;
}


public Embebidas(int linea, int columna, string nombre, Expresion argumento, Expresion argumento2)
:base(linea, columna, TipoE.EMBEBIDAS){

    Nombre = nombre;
    Argumento = argumento;
    Argumento2 = argumento2;
}

    private TipoRetorno Atoi(GenARM gen){

        return null;
    }

    private TipoRetorno ParseFloat(GenARM gen){
     
     return null;
     
     }

    private TipoRetorno TypeOf(GenARM gen){

       return null;
    }
    
   private TipoRetorno Len(GenARM gen){

    return null;
   }
   private TipoRetorno Join(GenARM gen){

   return null;
   }
   private TipoRetorno Index_(GenARM gen){

    return null;
   }

   private TipoRetorno Append(GenARM gen){

     return null;
     
       }


    public override TipoRetorno Interpretar(GenARM gen)
    {

        return null;
}
}