using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;

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

    public override TipoRetorno Interpretar(Entorno e)
    {
        return Signo switch {

            "!" => Not(e), 
            "&&" => And(e), 
            "||" => Or(e), 
            _ => new TipoRetorno("nil", Tipo.NIL) 
        };
    }

public TipoRetorno Not(Entorno e){
   
     TipoRetorno valor2 = Op2.Interpretar(e);
    
    Tipo tipo = valor2.Tipobase;
    if(tipo != Tipo.NIL && tipo == Tipo.BOOL){

        return new TipoRetorno (!valor2.Valor.Equals("true")?"true":"false", tipo);
    }
    e.GuardarError("Tipos Erroneos para not" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);

}


public TipoRetorno And(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones5[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

         return new TipoRetorno (valor2.Valor.Equals("true") && valor1.Valor.Equals("true")?"true":"false", tipo);
    }

    e.GuardarError("Tipos Erroneos para and" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);

}
public TipoRetorno Or(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones5[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

         return new TipoRetorno (valor2.Valor.Equals("true") || valor1.Valor.Equals("true")?"true":"false", tipo);
    }

    e.GuardarError("Tipos Erroneos para Or" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}


}