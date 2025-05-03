using System.Reflection.Metadata.Ecma335;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;

public class Entorno{

private Entorno? Padre;
public string Nombre;

private Dictionary <string, Variable> Variables = new ();

private Dictionary < string, Funcion > Funciones = new ();

public Entorno (Entorno? padre, string nombre){

Padre=padre;
Nombre = nombre;

}

public bool Guardarfuncion(Funcion func){
    if (!Funciones.ContainsKey(func.Nombre)){
        Funciones [func.Nombre] = func;
        Consola.TablaSimbolos.Add(new string []{Nombre, func.Nombre, func.Tiporetorno.Tipobase != Tipo.NIL? "Funcion ":"Metodo", func.Tiporetorno.GetTipo(), func.Linea.ToString(), func.Columna.ToString()});
        return true; 
    } return false;
}

public bool GuardarVariable(string nombre, TipoRetorno valor, Tipodato tipo, int linea, int columna){


    if(!Variables.ContainsKey(nombre)){

        Variables[nombre] = new Variable(valor, nombre, tipo);
        Consola.TablaSimbolos.Add(new string []{Nombre, nombre, "Variable", tipo.GetTipo(), linea.ToString(), columna.ToString()});
        return true; 
    }
    
    return false ; 

}



public bool ActualizarVariable(string nombre, TipoRetorno valor, int linea, int columna){
    
    Entorno? actual = this; 
    while(actual != null){

        if(actual.Variables.ContainsKey(nombre)){     
                
         actual.Variables[nombre] .Valor = valor;
         return true;
        }

        actual = actual.Padre;
    }

   GuardarError($"Asignacion de valor a variable inexistente '{nombre}'", linea, columna);
    return false;
}

//Sobre carga para actualizar posiciones de un sl
public bool ActualizarVariable(string nombre, List<int[]> indices ,TipoRetorno valor, int linea, int columna){
    
    Entorno? actual = this; 
    while(actual != null){

        if(actual.Variables.ContainsKey(nombre)){     
                
            Variable miVariable =  actual.Variables[nombre]; 
            if(miVariable.Valor.Tipobase == Tipo.SLICE){
                if(miVariable.Valor.Dimensiones >= indices.Count){
                    TipoRetorno posicion = miVariable.Valor.ObtenerPosicion(actual, nombre, indices);
                    if(posicion != null){
                        if(posicion.Tipobase != Tipo.SLICE && 
                        (posicion.Tipobase==valor.Tipobase || posicion.Tipobase == Tipo.FLOAT && valor.Tipobase == Tipo.INT)){
                            posicion.Valor = valor.Valor;
                            return true;
                        }
                        if(posicion.Tipobase == Tipo.SLICE && posicion.Tipobase == valor.Tipobase && 
                        (posicion.Tiposecundario == valor.Tiposecundario || posicion.Tiposecundario == Tipo.FLOAT && valor.Tiposecundario == Tipo.INT) &&
                        posicion.Dimensiones == valor.Dimensiones){
                            if(posicion.Tiposecundario == Tipo.FLOAT && valor.Tiposecundario == Tipo.INT){
                                valor.Intfloat();
                            }
                            posicion.Valor = valor.Valor;
                            return true;
                        }

                        if(valor.Tipobase != Tipo.SLICE){

                            GuardarError($"Los tipos no coinciden en la asignacion '{nombre}'", linea, columna);
                            }else if (!(valor.Tiposecundario == posicion.Tiposecundario || (posicion.Tiposecundario==Tipo.FLOAT && valor.Tiposecundario == Tipo.INT))){
                            GuardarError($"Los tipos no coinciden en la asignacion '{nombre}'", linea, columna);
                            }else{
                            GuardarError($"Las dimensiones del vector no son las esperadas '{nombre}'", linea, columna);
                            }
                        

                    }

                    return false;
                }
                GuardarError($"Cantidad de indices mayor a las dimensiones '{nombre}'", linea, columna);
                return false;
            }    
            GuardarError($"Asignacion con indices a una variable que no es slice '{nombre}'", linea, columna);
            return false;
        }

        actual = actual.Padre;
    }

   GuardarError($"Asignacion de valor a variable inexistente '{nombre}'", linea, columna);
    return false;
}

public Variable? ObtenerVariable(string nombre, int linea, int columna){

 Entorno? actual = this; 
    while(actual != null){

        if(actual.Variables.ContainsKey(nombre)){     

            return actual.Variables[nombre];
        }

        actual = actual.Padre;
    }


    GuardarError($"Llamada a variable inexistente '{nombre}'", linea, columna);
    return null; 

}

//Sobrecarga: mismo nombre para la funcion pero recibe diferente parametro, se utliza para otra cosa.

//Posiciones de vector
public Variable? ObtenerVariable(string nombre, List <int[]> indices, int linea, int columna){

 Entorno? actual = this; 
    while(actual != null){

        if(actual.Variables.ContainsKey(nombre)){     

            Variable miVariable = actual.Variables[nombre];
            if(miVariable.Valor.Tipobase == Tipo.SLICE){
                
                if(miVariable.Valor.Dimensiones >= indices.Count){
                    TipoRetorno posicion = miVariable.Valor.ObtenerPosicion(actual, nombre, indices);
                    if(posicion != null ){
                        return new Variable(posicion, nombre, miVariable.Valor.GetTipo());
                    }
                    return null;
                }
                GuardarError($"Cantiadada de indices sobrepasa las dimenciones '{nombre}'", linea, columna);
                return null;

            }

            
            GuardarError($"llamada por indices a una variable que no es slice '{nombre}'", linea, columna);
             return null;
        }

        actual = actual.Padre;
    }


    GuardarError($"Llamada a variable inexistente '{nombre}'", linea, columna);
    return null; 

}
public Funcion? ObtenerFuncion (string nombre, int linea, int columna){

    //entorno es una lista enlasada

    Entorno? actual = this; 
    while(actual != null){

        if(actual.Funciones.ContainsKey(nombre)){     //Funciones es un diccionario

            return actual.Funciones[nombre];
        }

        actual = actual.Padre;
    }

    GuardarError($"Llamada de Funcion no declarada '{nombre}'", linea, columna);
    return null;

}

public void GuardarError(string descripcion, int linea, int columna){   //insertartando errores SEMNATICOS

    Consola.Errores.Add(new Error(linea, columna, TipoError.SEMANTICO, descripcion));

}

public void EnviarImpresion (string cadena){}




}