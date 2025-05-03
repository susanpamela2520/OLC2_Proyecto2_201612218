namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public enum TipoError{

    LEXICO, SINTACTICO, SEMANTICO,
}

public static class TipoErrorExtension {

public static string GetNombre(this TipoError Tipo){
    return Tipo switch {

        TipoError.LEXICO => "Lexico", 
        TipoError.SINTACTICO => "Sintactico", 
        _ => "Semantico",  //default 

    };
}

}