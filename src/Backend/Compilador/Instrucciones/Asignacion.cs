using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;

public class Asignacion:Instruccion {
    private string Variable;
    private Expresion Valor;
    private List <Expresion>? Indices;

    public Asignacion (int linea, int columna, string variable, Expresion valor) :
    base(linea, columna, TipoI.ASIGNACION){

        Variable= variable;
        Valor = valor; 
    }

    //sobrecarga para reasignar posiciones de slices
    public Asignacion (int linea, int columna, string variable, Expresion valor, List<Expresion> indices) :
    base(linea, columna, TipoI.ASIGNACION){
    
        Variable= variable;
        Valor = valor; 
        Indices = indices;
    
    }

    public override TipoRetorno? Interpretar(GenARM gen)
    {
        if(gen.Frame == null) {
            gen.AddComentario($"========== Asignacion Variable: {Variable} ==========="); 

            Valor.Interpretar(gen);
            gen.PopObjeto(R.x0);
            var (byteOffset, obj) = gen.GetObjeto(Variable);

            if(gen.EstaEnFuncion != null) {
                gen.Mov(R.x1, obj.Offset * 8);
                gen.Add(R.x1, R.x29, R.x1);
                gen.Str(R.x0, R.x1);
            } else {
                gen.Mov(R.x1, byteOffset);
                gen.Add(R.x1, R.sp, R.x1);
                gen.Str(R.x0, R.x1);
                gen.Push(R.x0);
                gen.PushObjeto(gen.ClonarObjeto(obj));
            }
         
            gen.AddComentario($"========== Fin Asignacion Variable: {Variable} ==========="); 
        }
        return null;

    }
}


 