using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Relacional:Expresion{
    public Expresion Op1;
    public Expresion Op2;
    public string Signo;

    public Relacional (int linea, int columna, Expresion op1, string signo, Expresion op2) :
    base(linea, columna, TipoE.RELACIONAL) {
        Op1 = op1;
        Op2 = op2;
        Signo = signo;
    }

    public override TipoRetorno Interpretar(GenARM gen) {
        gen.AddComentario($"----- Relacional ({Signo}) -----");
        Op1.Interpretar(gen); // | 2 |
        Op2.Interpretar(gen); // | 3 | 2 |

        // Operando derecho
        var derechaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var derechaString = gen.TopePila().Type == Tipo.STRING;
        var derecha = gen.PopObjeto(derechaFloat ? R.d0 : R.x0); // | 2 |
        var izquierdaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var izquierdaString = gen.TopePila().Type == Tipo.STRING;
        var izquierda = gen.PopObjeto(izquierdaFloat ? R.d1 : R.x1); // | |

        if(izquierdaString && derechaString) {
            switch(Signo) {
                case "==":
                    gen.CmpString();
                    break;
                case "!=":
                    gen.CmpStringNot();
                    break;
                default:
                    break;
            };
            gen.PushObjeto(gen.ObjetoBool());
            return null;
        }

        if(derechaFloat || izquierdaFloat){
            if( !izquierdaFloat ){
                gen.Scvtf(R.d1, R.x1);
            }
            if( !derechaFloat ){
                gen.Scvtf(R.d0, R.x0);
            }

            gen.Fcmp(R.d1, R.d0); // 3 + 2 = 5

            var etiquetaVerdaderaF = gen.GetEtiqueta();
            var etiquetaFinF = gen.GetEtiqueta();

            switch(Signo) {
                case "==":
                    gen.Beq(etiquetaVerdaderaF);
                    break;
                case "!=":
                    gen.Bne(etiquetaVerdaderaF);
                    break;
                case ">=":
                    gen.Bge(etiquetaVerdaderaF);
                    break;
                case "<=":
                    gen.Ble(etiquetaVerdaderaF);
                    break;
                case ">":
                    gen.Bgt(etiquetaVerdaderaF);
                    break;
                default:
                    gen.Blt(etiquetaVerdaderaF);
                    break;
            };

            gen.Mov(R.x0, 0);
            gen.Push(R.x0);
            gen.B(etiquetaFinF);
            gen.AddEtiqueta(etiquetaVerdaderaF);
            gen.Mov(R.x0, 1);
            gen.Push(R.x0);
            gen.AddEtiqueta(etiquetaFinF);
            gen.PushObjeto(gen.ObjetoBool());

            gen.AddComentario($"--- Fin Relacionales ({Signo}) ---");
        
            return null;
        }

        gen.Cmp(R.x1, R.x0);

        var etiquetaVerdadera = gen.GetEtiqueta();
        var etiquetaFin = gen.GetEtiqueta();

        switch(Signo) {
            case "==":
                gen.Beq(etiquetaVerdadera);
                break;
            case "!=":
                gen.Bne(etiquetaVerdadera);
                break;
            case ">=":
                gen.Bge(etiquetaVerdadera);
                break;
            case "<=":
                gen.Ble(etiquetaVerdadera);
                break;
            case ">":
                gen.Bgt(etiquetaVerdadera);
                break;
            default:
                gen.Blt(etiquetaVerdadera);
                break;
        };

        gen.Mov(R.x0, 0);
        gen.Push(R.x0);
        gen.B(etiquetaFin);
        gen.AddEtiqueta(etiquetaVerdadera);
        gen.Mov(R.x0, 1);
        gen.Push(R.x0);
        gen.AddEtiqueta(etiquetaFin);
        gen.PushObjeto(gen.ObjetoBool());

        gen.AddComentario($"--- Fin Relacionales ({Signo}) ---");
        return null;
    }
}