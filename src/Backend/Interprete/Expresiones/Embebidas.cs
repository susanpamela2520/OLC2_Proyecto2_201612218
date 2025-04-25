using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

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

    private TipoRetorno Atoi(Entorno e, GenARM gen){

        TipoRetorno valorArgumento = Argumento.Interpretar(e, gen);
        if(valorArgumento.Tipobase == Tipo.STRING){
            if(Regex.IsMatch(valorArgumento.Valor.ToString(),@"^\d+$")){
                return new TipoRetorno(int.Parse(valorArgumento.Valor.ToString()), Tipo.INT);
            }
            e.GuardarError($" El formato de la cadena no es numerico entero ", Argumento.Linea, Argumento.Columna);
            return new TipoRetorno("nil", Tipo.NIL);
        }
        e.GuardarError($"Tipo de argumento erroneo en Atoi '{valorArgumento.Tipobase.GetNombre()}'", Argumento.Linea, Argumento.Columna);
        return new TipoRetorno("nil", Tipo.NIL);
    }

    private TipoRetorno ParseFloat(Entorno e, GenARM gen){
     
     return null;
     
     }

    private TipoRetorno TypeOf(Entorno e, GenARM gen){

       return null;
    }
    
   private TipoRetorno Len(Entorno e, GenARM gen){

    return null;
   }
   private TipoRetorno Join(Entorno e, GenARM gen){

   return null;
   }
   private TipoRetorno Index_(Entorno e, GenARM gen){

    return null;
   }

   private TipoRetorno Append(Entorno e, GenARM gen){

     return null;
     
       }


    public override TipoRetorno Interpretar(Entorno e, GenARM gen)
    {

        return null;
}
}