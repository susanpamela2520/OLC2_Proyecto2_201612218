using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

public class Relacional:Expresion{

public Expresion Op1;
public Expresion Op2;

public string Signo;


public Relacional (int linea, int columna, Expresion op1, string signo, Expresion op2 )
:base(linea, columna, TipoE.RELACIONAL){

Op1 = op1;
Op2 = op2;
Signo = signo;

}

    public override TipoRetorno Interpretar(Entorno e)
    {
       
    }

public TipoRetorno igual(Entorno e){
   
   

}
public TipoRetorno diferente(Entorno e){
   

}
public TipoRetorno mayorigual(Entorno e){
   
}
public TipoRetorno menorigual(Entorno e){
   
   
}

public TipoRetorno mayor(Entorno e){
   
}
public TipoRetorno menor(Entorno e){
   
   
}


}