using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Logico:Expresion{
    public Expresion Op1;
    public Expresion Op2;
    public string Signo;

    public Logico (int linea, int columna, Expresion op1, string signo, Expresion op2) :
    base(linea, columna, TipoE.LOGICO) {
        Op1 = op1;
        Op2 = op2;
        Signo = signo;
    }
    public Logico (int linea, int columna, string signo, Expresion op2) :
    base(linea, columna, TipoE.LOGICO) {
        Op2 = op2;
        Signo = signo;
    }

    public override TipoRetorno Interpretar(GenARM gen) {
        switch (Signo) {
            case "&&":
                And(gen);
                break;
            case "||":
                Or(gen);
                break;
            case "!":
                Not(gen);
                break;
            default:
                break;
        }
        return null;
    }

    public TipoRetorno And(GenARM gen) {
        Op1.Interpretar(gen);
        Op2.Interpretar(gen);

        var etiquetaFalsa = gen.GetEtiqueta();
        var etiquetaFin = gen.GetEtiqueta();

        gen.PopObjeto(R.x0);
        gen.PopObjeto(R.x1);

        gen.Cmp(R.x0, 0);
        gen.Beq(etiquetaFalsa);
        gen.Cmp(R.x1, 0);
        gen.Beq(etiquetaFalsa);
        gen.Mov(R.x0, 1);
        gen.Push(R.x0);
        gen.B(etiquetaFin);
        gen.AddEtiqueta(etiquetaFalsa);
        gen.Mov(R.x0, 0);
        gen.Push(R.x0);
        gen.AddEtiqueta(etiquetaFin);

        gen.PushObjeto(gen.ObjetoBool());
        return null;

    }

    public TipoRetorno Or(GenARM gen) {
        Op1.Interpretar(gen);
        Op2.Interpretar(gen);

        var etiquetaVerdadera = gen.GetEtiqueta();
        var etiquetaFin = gen.GetEtiqueta();

        gen.PopObjeto(R.x0);
        gen.PopObjeto(R.x1);

        gen.Cmp(R.x0, 0);
        gen.Bne(etiquetaVerdadera);
        gen.Cmp(R.x1, 0);
        gen.Bne(etiquetaVerdadera);
        gen.Mov(R.x0, 0);
        gen.Push(R.x0);
        gen.B(etiquetaFin);
        gen.AddEtiqueta(etiquetaVerdadera);
        gen.Mov(R.x0, 1);
        gen.Push(R.x0);
        gen.AddEtiqueta(etiquetaFin);

        gen.PushObjeto(gen.ObjetoBool());
        return null;
    }

    public TipoRetorno Not(GenARM gen) {
        Op2.Interpretar(gen);

        var etiquetaVerdadera = gen.GetEtiqueta();
        var etiquetaFin = gen.GetEtiqueta();

        gen.PopObjeto(R.x0);

        gen.Cmp(R.x0, 0);
        gen.Beq(etiquetaVerdadera);
        gen.Mov(R.x0, 0);
        gen.Push(R.x0);
        gen.B(etiquetaFin);
        gen.AddEtiqueta(etiquetaVerdadera);
        gen.Mov(R.x0, 1);
        gen.Push(R.x0);
        gen.AddEtiqueta(etiquetaFin);

        gen.PushObjeto(gen.ObjetoBool());
        return null;
    }
}