
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

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
public abstract TipoRetorno? Interpretar (GenARM gen);

}