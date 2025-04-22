using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;

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

    private TipoRetorno Atoi(Entorno e){

        TipoRetorno valorArgumento = Argumento.Interpretar(e);
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

    private TipoRetorno ParseFloat(Entorno e){
     
     TipoRetorno valorArgumento = Argumento.Interpretar(e);
        if(valorArgumento.Tipobase == Tipo.STRING){
            if(Regex.IsMatch(valorArgumento.Valor.ToString(),@"^\d+(\.\d+)?$")){
                return new TipoRetorno(double.Parse(valorArgumento.Valor.ToString(), CultureInfo.InvariantCulture), Tipo.FLOAT);
            
            }
            e.GuardarError($" El formato de la cadena no es numerico entero o decimal ", Argumento.Linea, Argumento.Columna);
            return new TipoRetorno("nil", Tipo.NIL);
        }
        e.GuardarError($"Tipo de argumento erroneo en ParseFloat '{valorArgumento.Tipobase.GetNombre()}'", Argumento.Linea, Argumento.Columna);
        return new TipoRetorno("nil", Tipo.NIL);
     
     }

    private TipoRetorno TypeOf(Entorno e){

        TipoRetorno valorArgumento = Argumento.Interpretar(e);
            if(valorArgumento.Tipobase == Tipo.SLICE){
                return new TipoRetorno( string.Concat(Enumerable.Repeat("[]", valorArgumento.Dimensiones)) + valorArgumento.Tiposecundario?.GetNombre(), Tipo.STRING);
            }
            if(valorArgumento.Tipobase == Tipo.STRUCT){
                
            }
        return new TipoRetorno(valorArgumento.Tipobase.GetNombre(), Tipo.STRING);

    }
    
   private TipoRetorno Len(Entorno e){

    TipoRetorno valor = Argumento.Interpretar(e);

    if(valor.Tipobase == Tipo.SLICE){
        return new TipoRetorno(((List <TipoRetorno>)valor.Valor).Count, Tipo.INT);
    }
        e.GuardarError ($"El tipo de dato no es valido para len", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);
   }
   private TipoRetorno Join(Entorno e){

    TipoRetorno valor = Argumento.Interpretar(e);

    if(valor.Tipobase == Tipo.SLICE){
        if(valor.Tiposecundario ==Tipo.STRING){
            if(valor.Dimensiones == 1){

                TipoRetorno valor2 = Argumento2.Interpretar(e);
                
                if(valor2.Tipobase == Tipo.STRING){

                    List<string> joined = new();
                    
                    //Se extraen los valores para insertar en la lista joined
                    foreach(TipoRetorno valor_ in (List <TipoRetorno>)valor.Valor){
                        joined.Add(valor_.Valor.ToString());
                    }
                    return new TipoRetorno(string.Join(valor2.Valor.ToString(), joined), Tipo.STRING);
                }
                 e.GuardarError ($"El delimitador debe ser de tipo string para slices.Join", Argumento2.Linea, Argumento2.Columna);
                 return new TipoRetorno("nil", Tipo.NIL);
    
            }
        e.GuardarError ($"El slice debe ser de unan dimension para slices.Join", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);
    
        }
         e.GuardarError ($"El slice debe ser de tipo string para slices.Join", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);
    }
        e.GuardarError ($"El tipo de dato no es valido para slices.Join", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);

   }
   private TipoRetorno Index_(Entorno e){

    TipoRetorno valor = Argumento.Interpretar(e);

    if(valor.Tipobase == Tipo.SLICE){
      
            if(valor.Dimensiones == 1){

                TipoRetorno valor2 = Argumento2.Interpretar(e);
                
                if(valor.Tiposecundario == valor2.Tipobase){

                  int indiceEncontrado = -1;
                  int indice = 0;
                    
                    //Se extraen los valores para insertar en la lista joined
                    foreach(TipoRetorno valor_ in (List <TipoRetorno>)valor.Valor){
                        if(valor_.Valor.Equals(valor2.Valor)){
                            indiceEncontrado = indice;
                            break;
                        }
                        indice ++;
                    }
                    return new TipoRetorno(indiceEncontrado, Tipo.INT);
                }
                 e.GuardarError ($"El tipo del Slice no coincide con el valor buscado", Argumento2.Linea, Argumento2.Columna);
                 return new TipoRetorno("nil", Tipo.NIL);
    
            }
        e.GuardarError ($"El slice debe ser de unan dimension para slices.Index", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);
    
        }
        
        e.GuardarError ($"El tipo de dato no es valido para slices.Index", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);
   }

   private TipoRetorno Append(Entorno e){

    TipoRetorno valor = Argumento.Interpretar(e);

    if(valor.Tipobase == Tipo.SLICE){
      
            if(valor.Dimensiones >= 1){

                TipoRetorno valor2 = Argumento2.Interpretar(e);
                if(valor.Dimensiones == valor2.Dimensiones + 1){
                    if(valor2.Tipobase == Tipo.SLICE && (valor.Tiposecundario == valor2.Tiposecundario || valor.Tiposecundario == Tipo.FLOAT && valor2.Tiposecundario == Tipo.INT) ||
                     valor.Tiposecundario == valor2.Tipobase ||  valor.Tiposecundario == Tipo.FLOAT &&  valor2.Tipobase == Tipo.INT){

                        if(valor2.Tipobase == Tipo.SLICE && valor.Tiposecundario == Tipo.FLOAT && valor2.Tiposecundario == Tipo.INT){
                            valor2.Intfloat();
                            
                        }else if(valor.Tiposecundario == Tipo.FLOAT &&  valor2.Tipobase == Tipo.INT){
                            valor2.Tipobase = Tipo.FLOAT;
                        }

                        ((List <TipoRetorno>)valor.Valor).Add(valor2);
                        
                        return new TipoRetorno(valor.Valor, valor.Tipobase, valor.Tiposecundario, valor.Dimensiones);
                    }
                    e.GuardarError ($"El tipo del Slice no coincide con el valor buscado", Argumento2.Linea, Argumento2.Columna);
                    return new TipoRetorno("nil", Tipo.NIL);
         }
                    e.GuardarError ($"El valor que intenta insertar es dei igual o mas dimensiones que el slice actual", Argumento2.Linea, Argumento2.Columna);
                    return new TipoRetorno("nil", Tipo.NIL);
                
            }
        e.GuardarError ($"El slice debe ser de una  dimension como minimo para Append", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);
    
        }
        
        e.GuardarError ($"El tipo de dato no es valido para Append", Argumento.Linea, Argumento.Columna);
         return new TipoRetorno("nil", Tipo.NIL);

   }


    public override TipoRetorno Interpretar(Entorno e)
    {

        return Nombre switch{
            "strconv.Atoi" => Atoi(e), 
            "strconv.ParseFloat" => ParseFloat(e), 
            "len" => Len(e), 
            "strings.Join" => Join(e), 
            "slices.Index" => Index_(e), 
            "append" => Append(e), 
            _ => TypeOf(e), 
        };
    }


}