namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class StackObject {
    public Tipo Type;
    public int Length { get; set; }
    public int Depth { get; set; }
    public string? Id { get; set; }
}