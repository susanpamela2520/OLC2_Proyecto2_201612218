namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class GenARM {
    private int ContadorEtiquetas = 0;
    private List<string> Instrucciones = new();
    private readonly StandardLibrary stdLib = new();
    private List<StackObject> stack = new();
    private int depth = 0;

    public GenARM() {
        IniciarPrograma();
    }

    // Operaciones con la Pila
    public void PushObjeto(StackObject obj) {
        stack.Add(obj);
    }

    public StackObject PopObjeto(R rd) {
        var obj = stack.Last();
        stack.RemoveAt(stack.Count - 1);
        Pop(rd);
        return obj;
    }

    public StackObject ObjetoInt() {
        return new StackObject {
            Type = Tipo.INT,
            Length = 8,
            Depth = depth,
            Id = null,
        };
    }

    public StackObject ObjetoFloat() {
        return new StackObject {
            Type = Tipo.FLOAT,
            Length = 8,
            Depth = depth,
            Id = null,
        };
    }

    public StackObject ObjetoBool() {
        return new StackObject {
            Type = Tipo.BOOL,
            Length = 8,
            Depth = depth,
            Id = null,
        };
    }

    public StackObject ObjetoRune() {
        return new StackObject {
            Type = Tipo.RUNE,
            Length = 8,
            Depth = depth,
            Id = null,
        };
    }

    public StackObject ObjetoString() {
        return new StackObject {
            Type = Tipo.STRING,
            Length = 8,
            Depth = depth,
            Id = null,
        };
    }

    public StackObject ClonarObjeto(StackObject obj) {
        return new StackObject {
            Type = obj.Type,
            Length = obj.Length,
            Depth = obj.Depth,
            Id = obj.Id,
        };
    }

    // Operaciones con Entorno
    public void NuevoEntorno() {
        depth ++;
    }

    public int TerminarEntorno() {
        int byteOffset = 0;

        for(int i = stack.Count - 1; i >= 0; i --) {
            if(stack[i].Depth == depth) {
                byteOffset += stack[i].Length;
                stack.RemoveAt(i);
            } else {
                break;
            }
        }

        depth --;
        return byteOffset;
    }

    public void NombrarObjeto(string id) {
        stack.Last().Id = id;
    }

    public (int, StackObject) GetObjeto(string id) {
        int byteOffset = 0;
        for(int i = stack.Count - 1; i >= 0; i --) {
            if(stack[i].Id == id) {
                return (byteOffset, stack[i]);
            }

            byteOffset += stack[i].Length;
        }

        return (-1, null);
    }

    public void Push(R rd) {
        Instrucciones.Add($"\tstr {rd}, [sp, #-8]!");
    }

    public void Pop(R rs) {
        Instrucciones.Add($"\tldr {rs}, [sp], #8");
    }

    //Genera una nueva etiqueta
    public string GetEtiqueta() {
        string etiqueta = "L" + ContadorEtiquetas;
        ContadorEtiquetas ++;
        return etiqueta;
    }

    //Agrega una etiqueta al codigo
    public void AddEtiqueta(string label) {
        Instrucciones.Add($"{label}:");
    }

    //Agrega un comentario al codigo
    public void AddComentario(string comment) {
        Instrucciones.Add($"\t// {comment}");
    }

    // Operaciones
    public void Add(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tadd {rd}, {rs1}, {rs2}");
    }

    public void Sub(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tsub {rd}, {rs1}, {rs2}");
    }

    public void Mul(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tmul {rd}, {rs1}, {rs2}");
    }

    public void Div(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tdiv {rd}, {rs1}, {rs2}");
    }

    public void Addi(R rd, R rs1, R imm) {
        Instrucciones.Add($"\taddi {rd}, {rs1}, #{imm}");
    }

    // Operaciones de Memoria
    public void Str(R rs1, R rs2, int offset = 0) {
        Instrucciones.Add($"\tstr {rs1}, [{rs2}, #{offset}]");
    }

    public void Ldr(R rs1, R rs2, int offset = 0) {
        Instrucciones.Add($"\tldr {rs1}, [{rs2}, #{offset}]");
    }

    public void Mov(R r, int imm) {
        Instrucciones.Add($"\tmov {r}, #{imm}");
    }

    public void Movz(R r1, string valor, string valor2) {
        Instrucciones.Add($"\tmovz {r1}, {valor}, lsl {valor2}");
    }

    public void Movk(R r1, string valor1, string valor2) {
        Instrucciones.Add($"\tmovk {r1}, {valor1}, lsl {valor2}");
    }

    //Agrega una llamada de sistemas
    public void Svc() {
        Instrucciones.Add($"\tsvc #0");
    }

    public void Scvtf(R rd, R rs) {
        Instrucciones.Add($"\tscvtf {rd}, {rs}");
    }

    // Impresiones en Consola
    public void ImprimirInt(R rs) {
        stdLib.Use("print_integer");
        Instrucciones.Add($"\tmov x0, {rs}");
        Instrucciones.Add("\tbl print_integer");
    }

    public void ImprimirSalto() {
        stdLib.Use("print_new_line");
        Instrucciones.Add("\tbl print_new_line");
    }

    // Estructura del Programa
    public void IniciarPrograma() {
        Instrucciones.Add(".data");
        Instrucciones.Add("heap: .space 4096");
       Instrucciones.Add(".text");
        Instrucciones.Add(".global _start");
        Instrucciones.Add("_start:");
    }

    public void TerminarPrograma() {
        Mov(R.x0, 0);
        Mov(R.x8, 93);
        Svc();
    }

    //Genera la estructura final del codigo
    public void generarCodigo() {
        TerminarPrograma();
        Instrucciones.Add(@"

// libreria estandar");

Instrucciones.Add(stdLib.GetFunctionDefinitions());
    }

    //Obtiene el codigo como un string
    public string getCodigo() {
        return string.Join('\n', Instrucciones);
    }
}