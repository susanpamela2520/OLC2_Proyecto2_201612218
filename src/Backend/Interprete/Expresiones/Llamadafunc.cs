using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;


public class Llamadafunc:Expresion{

    private string Nombre;
    private List<Expresion> Argumentos;

    public Llamadafunc(int linea, int columna, string nombre, List<Expresion> argumentos):
    base(linea, columna, TipoE.LLAMADAFUNC){

        Nombre = nombre;
        Argumentos = argumentos;
        
    }

    public override TipoRetorno? Interpretar (Entorno e){  //e entorno que llega

    Funcion? funcion = e.ObtenerFuncion(Nombre, Linea, Columna);

    if(funcion != null){
        Entorno local = new (e, $"Funcion {Nombre}");
        if (funcion.Parametros.Count == Argumentos.Count){
            
//Asignacion y validacion de los valores a los parametros 

            for(int i = 0; i < Argumentos.Count; i++ ){
                TipoRetorno valor = Argumentos[i].Interpretar(e);
                Parametro  parametro = funcion.Parametros [i];

                //validacion para primitivos

                if (valor.Tipobase != Tipo.SLICE && (valor.Tipobase == parametro.Tipo.Tipobase || (parametro.Tipo.Tipobase == Tipo.FLOAT && valor.Tipobase == Tipo.INT))){

                      local.GuardarVariable(parametro.Nombre, valor, parametro.Tipo, Argumentos[i].Linea, Argumentos[i].Columna);
                      continue;
                }


                //validacion para slice 


                //validacion para errores
                
                    if(valor.Tipobase != Tipo.SLICE){
                        local.GuardarError($"Tipos de datos Erroneos para el parametro '{parametro.Nombre}'", Argumentos[i].Linea, Argumentos[i].Columna);
                    }else if (!(valor.Tiposecundario == parametro.Tipo.Tiposecundario || (parametro.Tipo.Tiposecundario==Tipo.FLOAT && valor.Tiposecundario == Tipo.INT))){
                        local.GuardarError($"Tipos de datos Erroneos para el parametro '{parametro.Nombre}'", Argumentos[i].Linea, Argumentos[i].Columna);
                    }else{
                        local.GuardarError($"Dimensiones Erroneas para el parametro '{parametro.Nombre}'", Argumentos[i].Linea, Argumentos[i].Columna);
                    }

                    return null;

            }

            //Ejecucion de las instrucciones de la funcion 

            TipoRetorno? retorno = funcion.BloqueIntrucciones.Interpretar(local);
            if(retorno!= null){
                if(retorno.Valor.Equals(TipoE.RETORNO)){
                    return null;
                }
                

                //validacion para primitivos
            
                if(funcion.Tiporetorno.Tipobase != Tipo.SLICE && (funcion.Tiporetorno.Tipobase == retorno.Tipobase || (funcion.Tiporetorno.Tipobase == Tipo.FLOAT && retorno.Tipobase == Tipo.INT)) ){
                    return retorno;
                }

                //validacion para slice

                //validacion para errores 

                if(funcion.Tiporetorno.Tipobase != Tipo.SLICE){
                        local.GuardarError($"Tipos de datos Erroneos para el parametro '{Nombre}'", funcion.Linea, funcion.Columna);
                    }else if (!(funcion.Tiporetorno.Tiposecundario == retorno.Tiposecundario || (funcion.Tiporetorno.Tiposecundario ==Tipo.FLOAT && retorno.Tiposecundario == Tipo.INT))){
                        local.GuardarError($"Tipos de datos Erroneos para el parametro '{Nombre}'", funcion.Linea, funcion.Columna);
                    }else{
                        local.GuardarError($"Dimensiones Erroneas para el parametro '{Nombre}'", funcion.Linea, funcion.Columna);
                    }

                    return new TipoRetorno("nil", Tipo.NIL);

            }
            
            return null;
        }

        local.GuardarError("Cantidad Incorrecta de Parametros", Linea, Columna);
        return null;
    }


    return null;
    }

}