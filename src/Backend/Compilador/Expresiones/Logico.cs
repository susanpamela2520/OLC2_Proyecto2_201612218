using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Logico:Expresion{

public Expresion Op1;
public Expresion Op2;

public string Signo;


public Logico (int linea, int columna, Expresion op1, string signo, Expresion op2 )
:base(linea, columna, TipoE.LOGICO){

Op1 = op1;
Op2 = op2;
Signo = signo;

}
public Logico (int linea, int columna, string signo, Expresion op2 )
:base(linea, columna, TipoE.LOGICO){

Op2 = op2;
Signo = signo;

}

    public override TipoRetorno Interpretar(GenARM gen)
    {
        return null;
        
    }

public TipoRetorno Not(GenARM gen){
   
    return null;
}


public TipoRetorno And(GenARM gen){
   
    return null;

}
public TipoRetorno Or(GenARM gen){
   
    return null;
}


}