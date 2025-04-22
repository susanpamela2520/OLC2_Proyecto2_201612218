using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;


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

                if(Tipo != null ){

                        //Declaraciones con tipo y valor
                        if(Valor != null){

                            TipoRetorno retorno = Valor.Interpretar(e);

                            if (retorno.Tipobase != Utils.Tipo.SLICE && (retorno.Tipobase == Tipo.Tipobase|| Tipo.Tipobase == Utils.Tipo.FLOAT && retorno.Tipobase == Utils.Tipo.INT)){
                                if(Tipo.Tipobase == Utils.Tipo.FLOAT && retorno.Tipobase == Utils.Tipo.INT){
                                        retorno.Tipobase = Tipo.Tipobase;
                                }
                                e.GuardarVariable(Nombre, retorno, Tipo, Linea, Columna);
                                return null;

                            } 

                            if(retorno.Tipobase == Utils.Tipo.SLICE && retorno.Tipobase == Tipo.Tipobase && retorno.Dimensiones == Tipo.Dimensiones && (retorno.Tiposecundario == Tipo.Tiposecundario || Tipo.Tiposecundario == Utils.Tipo.FLOAT && retorno.Tiposecundario == Utils.Tipo.INT)){
                                if(Tipo.Tiposecundario == Utils.Tipo.FLOAT && retorno.Tiposecundario == Utils.Tipo.INT){
                                    retorno.Intfloat();
                                }
                                e.GuardarVariable(Nombre, retorno, retorno.GetTipo(), Linea, Columna);
                            
                             return null;
                            }
                       
                            e.GuardarError("Tipos Erroneos en la declaracion", Linea, Columna);
                            return null; 

                            //Declaraciones con tipo y sin valor
                        }else{
                            
                            TipoRetorno retorno = Tipo.Tipobase switch{

                                Utils.Tipo.INT => new Primitivo(0,0, 0, Tipo.Tipobase).Interpretar(e),
                                Utils.Tipo.FLOAT => new Primitivo(0,0, 0.0, Tipo.Tipobase).Interpretar(e),
                                Utils.Tipo.BOOL => new Primitivo(0,0, "false", Tipo.Tipobase).Interpretar(e),
                                Utils.Tipo.STRING => new Primitivo(0,0, "", Tipo.Tipobase).Interpretar(e),
                                _ => new Primitivo(0,0, '\0', Tipo.Tipobase).Interpretar(e),

                            };

                            e.GuardarVariable(Nombre, retorno, Tipo, Linea, Columna);
                            return null;

                        }

//Declaraciones sin tipo y con valor
                }else{
                    TipoRetorno retorno = Valor.Interpretar(e);
                    //Console.WriteLine(retorno.Valor);

                     e.GuardarVariable(Nombre, retorno, retorno.GetTipo(), Linea, Columna);
                            return null;
                }

    }


}