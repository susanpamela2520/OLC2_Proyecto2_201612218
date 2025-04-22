using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;

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
        return Signo switch {

            "==" => igual(e), 
            "!=" => diferente(e), 
            ">=" => mayorigual(e), 
            "<=" => menorigual(e), 
            ">" => mayor(e), 
            "<" => menor(e), 
            _ => new TipoRetorno("nil", Tipo.NIL) 
        };
    }

public TipoRetorno igual(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones3[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

         return new TipoRetorno(valor1.Valor.Equals(valor2.Valor)? "true":"false", tipo);
    }

    e.GuardarError("Tipos Erroneos para igualdad" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);

}
public TipoRetorno diferente(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones3[T1][T2]:Tipo.NIL;
     if(tipo != Tipo.NIL){

         return new TipoRetorno(!valor1.Valor.Equals(valor2.Valor)? "true":"false", tipo);
    }

    e.GuardarError("Tipos Erroneos para desigualdad" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);

}
public TipoRetorno mayorigual(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones4[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        string resultado =
            (valor1.Valor is int || valor1.Valor is double ? double.Parse (valor1.Valor.ToString()):valor1.Valor.ToString().ToCharArray()[0])
            >= 
            (valor2.Valor is int || valor2.Valor is double ? double.Parse (valor2.Valor.ToString()):valor2.Valor.ToString().ToCharArray()[0])
            ? "true":"false";
            return new TipoRetorno(resultado, tipo);
    }

    e.GuardarError("Tipos Erroneos para mayor igual" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}
public TipoRetorno menorigual(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones4[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        string resultado =
            (valor1.Valor is int || valor1.Valor is double ? double.Parse (valor1.Valor.ToString()):valor1.Valor.ToString().ToCharArray()[0])
            <= 
            (valor2.Valor is int || valor2.Valor is double ? double.Parse (valor2.Valor.ToString()):valor2.Valor.ToString().ToCharArray()[0])
            ? "true":"false";
            return new TipoRetorno(resultado, tipo);
    }

    e.GuardarError("Tipos Erroneos para menor igual" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}

public TipoRetorno mayor(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones4[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        string resultado =
            (valor1.Valor is int || valor1.Valor is double ? double.Parse (valor1.Valor.ToString()):valor1.Valor.ToString().ToCharArray()[0])
            > 
            (valor2.Valor is int || valor2.Valor is double ? double.Parse (valor2.Valor.ToString()):valor2.Valor.ToString().ToCharArray()[0])
            ? "true":"false";
            return new TipoRetorno(resultado, tipo);   
    }

    e.GuardarError("Tipos Erroneos para mayor que" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}
public TipoRetorno menor(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones4[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        string resultado =
            (valor1.Valor is int || valor1.Valor is double ? double.Parse (valor1.Valor.ToString()):valor1.Valor.ToString().ToCharArray()[0])
            < 
            (valor2.Valor is int || valor2.Valor is double ? double.Parse (valor2.Valor.ToString()):valor2.Valor.ToString().ToCharArray()[0])
            ? "true":"false";
            return new TipoRetorno(resultado, tipo);   
    }

    e.GuardarError("Tipos Erroneos para menor que" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}


}