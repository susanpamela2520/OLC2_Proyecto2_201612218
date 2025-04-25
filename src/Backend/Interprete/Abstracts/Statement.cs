using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;

public abstract class Statement{

public int Linea;
public int Columna;
public TipoStmt Tipostmt;
    protected Statement
    (
        int line,int columna, TipoStmt tipostmt
    ){

Linea = line;
Columna = columna;
Tipostmt = tipostmt;

    }
public abstract TipoRetorno? Interpretar (Entorno e, GenARM gen);

}