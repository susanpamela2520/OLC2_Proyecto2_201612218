using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;

public class Print:Instruccion {

    public List < Expresion> Expresiones;

    public Print (int linea, int columna, List < Expresion> expresiones ): 
    base (linea, columna, TipoI.PRINT){

        Expresiones = expresiones;
        
    }

    public override TipoRetorno? Interpretar(Entorno e, GenARM gen)
    {
       
       return null;

    }

}