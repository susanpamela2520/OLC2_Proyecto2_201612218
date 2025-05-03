using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class Print:Instruccion {

    public List < Expresion> Expresiones;

    public Print (int linea, int columna, List < Expresion> expresiones ): 
    base (linea, columna, TipoI.PRINT){

        Expresiones = expresiones;
        
    }

    public override TipoRetorno? Interpretar(Entorno e)
    {
        string impresion = string.Empty;
        foreach (Expresion exp in Expresiones){
            TipoRetorno valor = exp.Interpretar(e);
            // Console.WriteLine(valor.Valor + " " + valor.Tipobase + " " + valor.Tiposecundario + " " + valor.Dimensiones);
            if(valor.Tipobase == Tipo.SLICE){
               impresion = string.Format("{0} {1}", impresion, valor.ObtenerSlice());

            }else if(valor.Tipobase == Tipo.STRUCT){

            }else{
                impresion = string.Format("{0} {1}", impresion, valor.Valor);
            }
       
        //Console.WriteLine(valor.Valor);  
        } 

        e.EnviarImpresion($"{impresion}\n");
        return null;
    }

}