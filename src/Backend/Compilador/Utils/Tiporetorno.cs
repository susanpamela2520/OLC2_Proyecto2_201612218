using Antlr4.Runtime.Atn;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class TipoRetorno {

public object Valor;
public Tipo Tipobase;

public Tipo? Tiposecundario;

public int Dimensiones = 0;


public TipoRetorno (object valor, Tipo tipobase, Tipo? tiposecundario, int dimensiones ){

    Valor = valor;
    Tipobase = tipobase;
    Tiposecundario = tiposecundario;
    Dimensiones = dimensiones;

}

public TipoRetorno (object valor, Tipo tipobase ){

    Valor = valor;
    Tipobase = tipobase;

}

public Tipodato GetTipo(){
    return new Tipodato(Tipobase, Tiposecundario, Dimensiones);
}

public string  ObtenerSlice (){
    if(Tipobase!= Tipo.NIL){

        if(Tipobase == Tipo.SLICE){
            string slice = "[";
            List <TipoRetorno> sliceValores = (List <TipoRetorno>)Valor;
            for(int i =0; i<sliceValores.Count; i++){
                slice += sliceValores[i].ObtenerSlice();  //Esto lo hace recursivo 
                if(i<sliceValores.Count - 1){           // se concatenan las comas 
                slice += " ";
                }
            }
            return slice +"]";
        }else if (Tipobase == Tipo.STRUCT){

        }
    }
    return Valor.ToString();

}

public void Intfloat(){
    if((int) Tipobase < 5){
        Tipobase = Tipo.FLOAT;
    }else{
        List <TipoRetorno> sliceValores = (List <TipoRetorno>)Valor; 
        for(int i =0; i<sliceValores.Count; i++){
               sliceValores[i].Intfloat(); //aqui se hace recursivo            
               sliceValores[i].Tiposecundario = Tipo.FLOAT; 
        }
        Tiposecundario = Tipo.FLOAT;
    }

}

// public TipoRetorno ObtenerPosicion (Entorno e, string nombre, List <int[]> indices){

//     if(indices.Count > 0){
//         if(Tipobase == Tipo.SLICE  ){
//              List <TipoRetorno> sliceValores = (List <TipoRetorno>)Valor; 
//              int [] posiciones = indices [0];
//              indices.RemoveAt(0); 
//              if(sliceValores.Count > posiciones [0]){
//                 return sliceValores[posiciones[0]].ObtenerPosicion(e, nombre, indices);

//              }
//              e.GuardarError($"Indice fuera de rango en slices '{nombre}'", posiciones [1], posiciones [2]);
//              return new TipoRetorno("nil", Tipo.NIL);
//         }
//     }
//     return this;

// }
    
}