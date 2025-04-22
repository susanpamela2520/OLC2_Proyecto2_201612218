using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;

public abstract class Instruccion:Statement{

    public TipoI Tipoi;

    public Instruccion
    (
        int linea,int columna, TipoI tipoi
    ):base(linea, columna, TipoStmt.INSTRUCCION){
    Tipoi = tipoi;

    }
public abstract override TipoRetorno? Interpretar (Entorno e);

}