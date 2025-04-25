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

    //mueve valor a un registro
    public void Mov(R r, string value) {
        MainARM.Add($"\tmov {r}, {value}");
    }

    //mueve el valor de un registro a otro registro
    public void Mov(R r1, R r2) {
        MainARM.Add($"\tmov {r1}, {r2}");
    }

    //Agrega una llamada de sistemas 
    public void Svc(string value) {
        MainARM.Add($"\tsvc {value}");
    }

    //Genera la estructura final del codigo 
    public void generarCodigo() {
        CodigoARM.Add(".data");
        CodigoARM.Add(".text");
        CodigoARM.Add(".global main");
        CodigoARM.AddRange(MainARM);
    }

    //Obtiene el codigo como un string 
    public string getCodigo() {
        return string.Join('\n', CodigoARM);
    }
}