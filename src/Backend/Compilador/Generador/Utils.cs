namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;

public static class Utils {
    public static List<byte> StringToByteArray(string str) {
        var resultado = new List<byte>();
        int elementIndex = 0;

        while(elementIndex < str.Length) {
            resultado.Add((byte) str[elementIndex]);
            elementIndex++;
        }

        resultado.Add(0);
        return resultado;
    }
}