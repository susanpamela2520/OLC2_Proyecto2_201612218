namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public enum Tipo{
INT = 0, 
FLOAT = 1, 
STRING = 2, 
BOOL = 3,
RUNE = 4, 
SLICE = 5,
STRUCT = 6,
NIL = 7, 
}

public static class TipoExtension {
    public static string GetNombre(this Tipo tipo) {
        return tipo switch {
            Tipo.INT => "int",
            Tipo.FLOAT => "float64",
            Tipo.BOOL => "bool",
            Tipo.RUNE => "rune",
            Tipo.STRING => "string",
            Tipo.SLICE => "slice",
            Tipo.STRUCT => "struct",
            _ => "nil",
        };
    }
}