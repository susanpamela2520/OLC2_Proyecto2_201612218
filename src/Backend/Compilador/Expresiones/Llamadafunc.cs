using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Llamadafunc:Expresion{

    private string Nombre;
    private List<Expresion> Argumentos;

    public Llamadafunc(int linea, int columna, string nombre, List<Expresion> argumentos):
    base(linea, columna, TipoE.LLAMADAFUNC){

        Nombre = nombre;
        Argumentos = argumentos;
        
    }

    public override TipoRetorno? Interpretar(GenARM gen){  //e entorno que llega
        gen.AddComentario($"----- Llamada Función: {Nombre} -----");
        var EtiquetaPostLlamada = gen.GetEtiqueta();

        // 1. | RA | FP |
        int baseOffset = 2;
        int stackElementSize = 8;

        gen.Mov(R.x0, baseOffset * stackElementSize);
        gen.Sub(R.sp, R.sp, R.x0);

        // 2. | RA | FP | ...params |
        foreach(var param in Argumentos) {
            param.Interpretar(gen);
        }

        // 3. Calcular el valor de FP
        // Regresar el SP al inicio del Frame
        gen.Mov(R.x0, stackElementSize * (baseOffset + Argumentos.Count));
        gen.Add(R.sp, R.sp, R.x0);

        // Calcular la posición donde se almacena el FP
        gen.Mov(R.x0, stackElementSize);
        gen.Sub(R.x0, R.sp, R.x0);

        gen.Adr(R.x1, EtiquetaPostLlamada);
        gen.Push(R.x1);

        // Alinear el SP al final del Frame
        int tamanoFrame = gen.Funciones[Nombre].TamanoFrame;
        gen.Mov(R.x0, (tamanoFrame - 2) * stackElementSize);
        gen.Sub(R.sp, R.sp, R.x0);

        gen.AddComentario($"Llamando Funcion: {Nombre}");
        gen.Bl(Nombre);
        gen.AddComentario($"Llamada Completada");
        gen.AddEtiqueta(EtiquetaPostLlamada);

        // Obtener el valor de Retorno
        var offsetReturn = tamanoFrame - 1;
        gen.Mov(R.x4, offsetReturn * stackElementSize);
        gen.Sub(R.x4, R.x29, R.x4);
        gen.Ldr(R.x4, R.x4);

        // 4. Regresar el FP al contexto de ejecución anterior
        gen.Mov(R.x1, stackElementSize);
        gen.Sub(R.x1, R.x29, R.x1);
        gen.Ldr(R.x29, R.x1);

        // 5. Regresar el SP al contexto de ejecución anterior
        gen.Mov(R.x0, stackElementSize * tamanoFrame);
        gen.Add(R.sp, R.sp, R.x0);

        // 6. Regresar el valor de Retorno
        gen.Push(R.x4);
        gen.PushObjeto(new StackObject{
            Type = gen.Funciones[Nombre].TipoRetorno,
            Id = null,
            Offset = 0,
            Length = 8,
        });

        gen.AddComentario($"--- Fin Llamada Función: {Nombre} ---");
        return null;
    }
}