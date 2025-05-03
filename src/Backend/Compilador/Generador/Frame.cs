namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;

public class Frame {
    public List<FrameElement> FrameElements = new();
    public int BaseOffset;
    public int LocalOffset = 0;

    public Frame(int baseOffset) {
        BaseOffset = baseOffset;
    }

    public int getOffsetTotal() {
        return BaseOffset + LocalOffset;
    }
}