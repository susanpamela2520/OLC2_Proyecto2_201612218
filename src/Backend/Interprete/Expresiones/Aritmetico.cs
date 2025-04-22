using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

public class Aritmetico:Expresion{

public Expresion Op1;
public Expresion Op2;

public string Signo;


public Aritmetico (int linea, int columna, Expresion op1, string signo, Expresion op2 )
:base(linea, columna, TipoE.ARITMETICO){

Op1 = op1;
Op2 = op2;
Signo = signo;

}

    public override TipoRetorno Interpretar(Entorno e)
    {
        
    }

public TipoRetorno suma(Entorno e){
   
    

    

    
}
public TipoRetorno resta(Entorno e){
   
   
    

    
}
public TipoRetorno multiplicacion(Entorno e){
   
    
}
public TipoRetorno division(Entorno e){
   
    
}

public TipoRetorno modulo(Entorno e){
   
    
}


}