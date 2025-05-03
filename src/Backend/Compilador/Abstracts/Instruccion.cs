
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

public abstract class Instruccion:Statement{

    public TipoI Tipoi;

    public Instruccion
    (
        int linea,int columna, TipoI tipoi
    ):base(linea, columna, TipoStmt.INSTRUCCION){
    Tipoi = tipoi;

    }
public abstract override TipoRetorno? Interpretar (GenARM gen);

}