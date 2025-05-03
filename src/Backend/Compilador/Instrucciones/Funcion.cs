namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

public class Funcion : Instruccion {
    public string Nombre;
    public List<Parametro> Parametros;
    public Instruccion BloqueIntrucciones;
    public Tipodato Tiporetorno;

    public Funcion(int linea, int columna, string nombre, List<Parametro> parametros, Instruccion instrucciones, Tipodato tipo) :
    base (linea, columna, TipoI.FUNCION) {
        Nombre = nombre;
        Parametros = parametros;
        BloqueIntrucciones = instrucciones;
        Tiporetorno = tipo;
    }

    public override TipoRetorno? Interpretar(GenARM gen) {
        if(!Nombre.Equals("main")) {
            /*
            
            | RA | FP-1 | ...parametros | ...variables locales | direccion de retorno |
            
            func ackermann(m int, n int, a int) {
                var a int = 10;
                ...
                return 1;
            }

            baseOffset = 2
            parametrosOffset = 3
            offsetLocal = 1
            returnOffset = 1
            tamanoFrame = 2 + 3 + 1 + 1 = 7

            */

            int baseOffset = 2;
            int parametrosOffset = Parametros.Count;

            Frame frame = new(baseOffset + parametrosOffset);
            gen.SetFrame(frame);

            BloqueIntrucciones.Interpretar(gen);

            gen.SetFrame(null);

            var frameTmp = frame.FrameElements;
            int offsetLocal = frameTmp.Count;
            int returnOffset = 1;

            int tamanoFrame = baseOffset + parametrosOffset + offsetLocal + returnOffset;

            gen.Funciones.Add(Nombre, new Metadata {
                TamanoFrame = tamanoFrame,
                TipoRetorno = Tiporetorno.Tipobase
            });

            var instruccionesAnteriores = gen.Instrucciones;
            gen.Instrucciones = new();

            var contadorParametros = 0;

            foreach(var param in Parametros) {
                gen.PushObjeto(new StackObject{
                    Type = param.Tipo.Tipobase,
                    Id = param.Nombre,
                    Offset = contadorParametros + baseOffset,
                    Length = 8,
                });
                contadorParametros ++;
            }

            foreach(FrameElement elemento in frameTmp) {
                gen.PushObjeto(new StackObject{
                    Type = Tipo.NIL,
                    Id = elemento.Nombre,
                    Offset = elemento.Offset,
                    Length = 8,
                }); 
            }

            gen.EstaEnFuncion = Nombre;
            gen.fpOffset = 0;

            gen.EtiquetaReturn = gen.GetEtiqueta();

            gen.AddComentario($"==== DECLARACION FUNCION: {Nombre} ====");

            gen.AddEtiqueta(Nombre);
            
            BloqueIntrucciones.Interpretar(gen);

            gen.AddEtiqueta(gen.EtiquetaReturn);

            gen.Add(R.x0, R.sp, R.xzr);
            gen.Ldr(R.x30, R.x0);
            gen.Br(R.x30);

            gen.AddComentario($"== FIN DECLARACION FUNCION: {Nombre} ==");

            for(int i = 0; i < parametrosOffset; i ++) {
                gen.PopObjeto();
            }

            gen.InstruccionesFunciones.AddRange(gen.Instrucciones);

            gen.Instrucciones = instruccionesAnteriores;

            gen.EstaEnFuncion = null;
        } else {
            BloqueIntrucciones.Interpretar(gen);
        }

        return null;
    }
}