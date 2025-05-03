namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Generador;

public class GenARM {
    private int ContadorEtiquetas = 0;
    private List<string> CodigoARM = new();
    public List<string> MainARM = new();
    public List<string> FuncionesARM = new();
    private HashSet<string> BuiltinsUsados = new();

    //Genera una nueva etiqueta
    public string GetEtiqueta() {
        string etiqueta = "L" + ContadorEtiquetas;
        ContadorEtiquetas ++;
        return etiqueta;
    }

    //Agrega una etiqueta al codigo
    public void AddEtiqueta(string label) {
        MainARM.Add(label + ":");
    }

    //Agrega un comentario al codigo
    public void AddComentario(string comment) {
        MainARM.Add("// " + comment);
    }

    //Mueve valor a un registro
    public void Mov(R r, string value) {
        MainARM.Add($"\tmov {r}, {value}");
    }

    //Mueve el valor de un registro a otro registro
    public void Mov(R r1, R r2) {
        MainARM.Add($"\tmov {r1}, {r2}");
    }

    public void Movz(R r1, string valor, string valor2) {
        MainARM.Add($"\tmovz {r1}, {valor}, lsl {valor2}");
    }

    public void Movk(R r1, string valor1, string valor2) {
        MainARM.Add($"\tmovk {r1}, {valor1}, lsl {valor2}");
    }

    //Cargas
    public void Ldr(R r1, string valor) {
        MainARM.Add($"\tldr {r1}, {valor}");
    }

    //Agrega una llamada de sistemas
    public void Svc(string value) {
        MainARM.Add($"\tsvc {value}");
    }

    public void Scvtf(R rd, R rs) {
        MainARM.Add($"\tscvtf {rd}, {rs}");
    }

    public void Push(R rd) {
        MainARM.Add($"\tstr {rd}, [sp, #-8]!");
    }

    public void Pop(R rs) {
        MainARM.Add($"\tldr {rs}, [sp], #8");
    }

    //Genera la estructura final del codigo
    public void generarCodigo() {
        CodigoARM.Add(".data");
        CodigoARM.Add("heap:");
        CodigoARM.Add(".text");
        CodigoARM.Add(".global main");
        CodigoARM.AddRange(MainARM);
    }

    //Obtiene el codigo como un string
    public string getCodigo() {
        return string.Join('\n', CodigoARM);
    }
}