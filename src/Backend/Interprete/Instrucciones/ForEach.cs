using OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;


public class ForEach:Instruccion{

private string Indice;
private string Valor;

private Expresion Slice; 
private Instruccion Bloque; 

public ForEach (int linea, int columna, string indice, string valor,  Expresion slice, Instruccion bloque):
    
    base(linea, columna, TipoI.FOR){

       Indice = indice;
       Valor = valor;
       Slice = slice; 
       Bloque = bloque;
    }


    public override TipoRetorno? Interpretar(Entorno e)
    {

        Entorno local = new (e, e.Nombre);
        int contadorIndice = 0;
        local.GuardarVariable(Indice, new Primitivo(0,0, contadorIndice, Tipo.INT).Interpretar(local), new Tipodato(Tipo.INT), Linea, Columna);

        TipoRetorno miSlice = Slice.Interpretar(local);
        local.GuardarVariable(Valor, new TipoRetorno(null, miSlice.Tipobase, miSlice.Tiposecundario, miSlice.Dimensiones ), miSlice.GetTipo(), Linea, Columna);

        List <TipoRetorno> miOtroSlice = (List <TipoRetorno>) miSlice.Valor;

        while(contadorIndice < miOtroSlice.Count){
            TipoRetorno miValor = miOtroSlice[contadorIndice];
            local.ActualizarVariable(Valor, new TipoRetorno(miValor.Valor, miValor.Tipobase, miValor.Tiposecundario, miValor.Dimensiones), 0,0);
              TipoRetorno bloque = Bloque.Interpretar(local);
                 if(bloque!= null){
                    if(bloque.Valor.Equals (TipoI.CONTINUE)){
                        contadorIndice ++; 
                        local.ActualizarVariable(Indice, new Primitivo(0,0, contadorIndice, Tipo.INT).Interpretar(local), 0,0);
                         continue;
                    }
                    if(bloque.Valor.Equals (TipoI.BREAK)){
                         break;
                    }
                    return bloque;
                 }

                contadorIndice ++; 
                local.ActualizarVariable(Indice, new Primitivo(0,0, contadorIndice, Tipo.INT).Interpretar(local), 0,0);
         }

        return null;
    }

}

  