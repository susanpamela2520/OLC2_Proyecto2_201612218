using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


//se encierra en llaves para crear los entornos hijos
public class Bloque:Instruccion{
  
    public List<Statement> Instrucciones;

    public Bloque (int linea, int columna, List<Statement> instrucciones):
    
    base(linea, columna, TipoI.BLOQUE){

        Instrucciones = instrucciones;

    }

    public override TipoRetorno? Interpretar(Entorno e)
    {
        
    }


}