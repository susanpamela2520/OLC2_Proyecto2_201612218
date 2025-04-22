using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;

public abstract class Expresion:Statement{

    public TipoE tipoE;
    public Expresion
    (
        int linea,int columna, TipoE tipoe
    ):base(linea, columna, TipoStmt.EXPRESION){

        tipoE = tipoe;

    }
public abstract override TipoRetorno Interpretar (Entorno e);

}