namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class GenARM {
    private int ContadorEtiquetas = 0;
    public List<string> Instrucciones = new();
    public List<string> InstruccionesFunciones = new();
    private readonly StandardLibrary stdLib = new();
    private List<StackObject> stack = new();
    private int depth = 0;

    public string? EtiquetaContinue = null;
    public string? EtiquetaBreak = null;
    public string? EtiquetaReturn = null;

    public string? EstaEnFuncion = null;
    public int fpOffset = 0;

    public Dictionary<string, Metadata> Funciones = new();

    public Frame? Frame;

    public GenARM() {
        IniciarPrograma();
    }

    // Frame de Función
    public void SetFrame(Frame? frame) {
        Frame = frame;
    }

    public StackObject GetFrameLocal(int indice) {
        var obj = stack.Where(o => o.Type == Tipo.NIL).ToList()[indice];
        return obj;
    }

    // Operaciones con la Pila
    public void PushObjeto(StackObject obj) {
        stack.Add(obj);
    }

    public StackObject TopePila (){
        return stack.Last();       
    }

    public StackObject PopObjeto(R rd) {
        var obj = stack.Last();
        stack.RemoveAt(stack.Count - 1);
        Pop(rd);
        return obj;
    }

    public void PopObjeto() {
        AddComentario("Sacando Objeto de la Pila");
        try {
            stack.RemoveAt(stack.Count - 1);
        } catch(System.Exception) {
            Console.WriteLine(this.ToString());
            throw new Exception("Pila Vacia");
        }
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


    //Busca objeto del stack que su id sea igual al del parametro.
    //devuelve una tupla (se puede definir el tipo)
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
        Instrucciones.Add($"\tsdiv {rd}, {rs1}, {rs2}");
    }

    public void Addi(R rd, R rs1, R imm) {
        Instrucciones.Add($"\taddi {rd}, {rs1}, #{imm}");
    }

    //comparacion
    public void Cmp(R rs1, R rs2) {
        Instrucciones.Add($"\tcmp {rs1}, {rs2} ");
    }

    public void Cmp(R rs1, int imm) {
        Instrucciones.Add($"\tcmp {rs1}, #{imm} ");
    }

    public void CmpString() {
        stdLib.Use("equal_string");
        Instrucciones.Add("\tbl equal_string");
    }

    public void CmpStringNot() {
        stdLib.Use("not_equal_string");
        Instrucciones.Add("\tbl not_equal_string");
    }

    public void Fcmp(R rs1, R rs2) {
        Instrucciones.Add($"\tfcmp {rs1}, {rs2} ");
    }
    
    public void Cbz(R rs, string etiqueta) {
        Instrucciones.Add($"\tcbz {rs}, {etiqueta} ");
    }

    //saltos
    public void Beq(string etiqueta){
        Instrucciones.Add($"\tbeq {etiqueta}");
    }

    public void Bne(string etiqueta){
        Instrucciones.Add($"\tbne {etiqueta}");
    }

    public void Blt(string etiqueta){
        Instrucciones.Add($"\tblt {etiqueta}");
    }

    public void Bgt(string etiqueta){
        Instrucciones.Add($"\tbgt {etiqueta}");
    }

    public void Ble(string etiqueta){
        Instrucciones.Add($"\tble {etiqueta}");
    }

    public void Bge(string etiqueta){
        Instrucciones.Add($"\tbge {etiqueta}");
    }

    public void Br(R rs){
        Instrucciones.Add($"\tbr {rs}");
    }

    public void Bl(string etiqueta){
        Instrucciones.Add($"\tbl {etiqueta}");
    }

    public void B(string etiqueta){
        Instrucciones.Add($"\tb {etiqueta}");
    }

    //Operaciones con flotantes 

    public void FAdd(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tfadd {rd}, {rs1}, {rs2}");
    }

    public void FSub(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tfsub {rd}, {rs1}, {rs2}");
    }

    public void FMul(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tfmul {rd}, {rs1}, {rs2}");
    }

    public void FDiv(R rd, R rs1, R rs2) {
        Instrucciones.Add($"\tfdiv {rd}, {rs1}, {rs2}");
    }

    public void Scvtf (R rd, R rs){
        Instrucciones.Add($"\tscvtf {rd}, {rs}");
    }


    // Operaciones de Memoria
    public void Adr(R rs, string etiqueta) {
        Instrucciones.Add($"\tadr {rs}, {etiqueta}");
    }

    public void Str(R rs1, R rs2, int offset = 0) {
        Instrucciones.Add($"\tstr {rs1}, [{rs2}, #{offset}]");
    }

    public void Strb(R rs1, R rs2) {
        Instrucciones.Add($"\tstrb {rs1}, [{rs2}]");
    }

    public void Ldr(R rs1, R rs2, int offset = 0) {
        Instrucciones.Add($"\tldr {rs1}, [{rs2}, #{offset}]");
    }

    public void Mov(R r, int imm) {
        Instrucciones.Add($"\tmov {r}, #{imm}");
    }

    public void FMov(R rd, R rs) {
        Instrucciones.Add($"\tfmov {rd}, {rs}");
    }

    public void Movz(R r1, int valor1, int valor2) {
        Instrucciones.Add($"\tmovz {r1}, #{valor1}, lsl #{valor2}");
    }

    public void Movk(R r1, int valor1, int valor2) {
        Instrucciones.Add($"\tmovk {r1}, #{valor1}, lsl #{valor2}");
    }

    //Agrega una llamada de sistemas
    public void Svc() {
        Instrucciones.Add($"\tsvc #0");
    }

    // Impresiones en Consola
    public void ImprimirInt(R rs) {
        stdLib.Use("print_integer");
        Instrucciones.Add($"\tmov x0, {rs}");
        Instrucciones.Add("\tbl print_integer");
    }
    public void ImprimirFloat() {
        stdLib.Use("print_double");
        stdLib.Use("print_integer");
        Instrucciones.Add("\tbl print_double");
    }

    public void ImprimirString(R rs) {
        stdLib.Use("print_string");
        Instrucciones.Add($"\tmov x0, {rs}");
        Instrucciones.Add("\tbl print_string");
    }

    public void ImprimirBool() {
        stdLib.Use("print_bool");
        Instrucciones.Add("\tbl print_bool");
    }

    public void ImprimirRune() {
        stdLib.Use("print_rune");
        Instrucciones.Add("\tbl print_rune");
    }

    public void ImprimirSalto() {
        stdLib.Use("print_new_line");
        Instrucciones.Add("\tbl print_new_line");
    }

    public void ImprimirEspacio() {
        stdLib.Use("print_space");
        Instrucciones.Add("\tbl print_space");
    }

    // Estructura del Programa
    public void IniciarPrograma() {
        Instrucciones.Add(".data");
        Instrucciones.Add("heap: .space 8192");
        Instrucciones.Add(".text");
        Instrucciones.Add(".global _start");
        Instrucciones.Add("_start:");
        Instrucciones.Add("\tadr x10, heap"); //x10 punteor de heap
    }

    public void TerminarPrograma() {
        Mov(R.x0, 0);
        Mov(R.x8, 93);
        Svc();
    }

    //Genera la estructura final del codigo
    public void generarCodigo() {
        TerminarPrograma();
        Instrucciones.Add("\n\n// Foreign Function");
        Instrucciones.AddRange(InstruccionesFunciones);

        Instrucciones.Add("\n\n// Standard Library");
        Instrucciones.Add(stdLib.GetFunctionDefinitions());
    }

    //Obtiene el codigo como un string
    public string getCodigo() {
        return string.Join('\n', Instrucciones);
    }
}