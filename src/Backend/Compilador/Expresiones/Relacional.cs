using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

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

    public override TipoRetorno Interpretar(GenARM gen)
    {
       return null;

    }

public TipoRetorno igual(GenARM gen){
   
   return null;

}
public TipoRetorno diferente(GenARM gen){
   
   return null;

}
public TipoRetorno mayorigual(GenARM gen){
   
return null;
}
public TipoRetorno menorigual(GenARM gen){
   return null;
   
}

public TipoRetorno mayor(GenARM gen){
   
   return null;
}
public TipoRetorno menor(GenARM gen){
   
   return null;
}


}