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
        return Signo switch {

            "+" => suma(e), 
            "-" => resta(e), 
            "*" => multiplicacion(e), 
            "/" => division(e), 
            "%" => modulo(e), 
            _ => new TipoRetorno("nil", Tipo.NIL) 
        };
    }

public TipoRetorno suma(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones1[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        if(tipo == Tipo.INT){
            return new TipoRetorno(int.Parse(valor1.Valor.ToString())+int.Parse(valor2.Valor.ToString()), tipo);
        }
        if(tipo == Tipo.FLOAT){
            return new TipoRetorno(double.Parse(valor1.Valor.ToString())+double.Parse(valor2.Valor.ToString()), tipo);
        }
        if(tipo == Tipo.STRING){
            return new TipoRetorno($"{valor1.Valor}{valor2.Valor}", tipo);
        }

    }

    e.GuardarError("Tipos Erroneos para sumas" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);

}
public TipoRetorno resta(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones2[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        if(tipo == Tipo.INT){
            return new TipoRetorno(int.Parse(valor1.Valor.ToString())-int.Parse(valor2.Valor.ToString()), tipo);
        }
        if(tipo == Tipo.FLOAT){
            return new TipoRetorno(double.Parse(valor1.Valor.ToString())-double.Parse(valor2.Valor.ToString()), tipo);
        }
    }

    e.GuardarError("Tipos Erroneos para restas" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);

}
public TipoRetorno multiplicacion(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;
   // Console.WriteLine(valor1.Tipobase);
    //Console.WriteLine(valor2.Tipobase);


    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones2[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        if(tipo == Tipo.INT){
            return new TipoRetorno(int.Parse(valor1.Valor.ToString())*int.Parse(valor2.Valor.ToString()), tipo);
        }
        if(tipo == Tipo.FLOAT){
            return new TipoRetorno(double.Parse(valor1.Valor.ToString())*double.Parse(valor2.Valor.ToString()), tipo);
        }
    }

    e.GuardarError("Tipos Erroneos para multiplicación" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}
public TipoRetorno division(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones2[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        if(tipo == Tipo.INT){
            return new TipoRetorno(int.Parse(valor1.Valor.ToString())/int.Parse(valor2.Valor.ToString()), tipo);
        }
        if(tipo == Tipo.FLOAT){
            return new TipoRetorno(double.Parse(valor1.Valor.ToString())/double.Parse(valor2.Valor.ToString()), tipo);
        }
    }

    e.GuardarError("Tipos Erroneos para division" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}

public TipoRetorno modulo(Entorno e){
   
    TipoRetorno valor1 = Op1.Interpretar(e);
    TipoRetorno valor2 = Op2.Interpretar(e);
    int T1 = (int) valor1.Tipobase;
    int T2 = (int) valor2.Tipobase;

    Tipo tipo = !(T1 > 4 || T2 > 4)? Operaciones.Operaciones2[T1][T2]:Tipo.NIL;
    if(tipo != Tipo.NIL){

        if(tipo == Tipo.INT){
            return new TipoRetorno(int.Parse(valor1.Valor.ToString())%int.Parse(valor2.Valor.ToString()), tipo);
        }
        
    }

    e.GuardarError("Tipos Erroneos para modulo" , Op1.Linea, Op1.Columna);
    return new TipoRetorno("nil", Tipo.NIL);
}


}