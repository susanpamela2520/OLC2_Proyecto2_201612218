
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

public abstract class Expresion:Statement{

    public TipoE tipoE;
    public Expresion
    (
        int linea,int columna, TipoE tipoe
    ):base(linea, columna, TipoStmt.EXPRESION){

        tipoE = tipoe;

    }
public abstract override TipoRetorno Interpretar (GenARM gen);

}