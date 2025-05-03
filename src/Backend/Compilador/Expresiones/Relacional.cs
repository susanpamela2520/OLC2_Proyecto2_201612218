using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Relacional:Expresion{

public Expresion Op1;
public Expresion Op2;

public string Signo;


public Relacional (int linea, int columna, Expresion op1, string signo, Expresion op2 )
:base(linea, columna, TipoE.RELACIONAL){

Op1 = op1;
Op2 = op2;
Signo = signo;

}

    public override TipoRetorno Interpretar(GenARM gen)
    {
      
 gen.AddComentario($"----- Relacional ({Signo}) -----");
        Op1.Interpretar(gen); // | 2 |
        Op2.Interpretar(gen); // | 3 | 2 |

        // Operando derecho
        var derechaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var derecha = gen.PopObjeto(derechaFloat? R.d0:R.x0); // | 2 |
        var izquierdaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var izquierda = gen.PopObjeto(izquierdaFloat? R.d1:R.x1); // | |

        if(derechaFloat || izquierdaFloat){
            if( !izquierdaFloat ){
                gen.Scvtf(R.d1, R.x1);
            }
            if( !derechaFloat ){
                gen.Scvtf(R.d0, R.x0);
            }

        gen.AddComentario($"--- Fin Relacionales ({Signo}) ---");
        
            return null;
        }

        // Suma de Enteros
        gen.Cmp(R.x1, R.x0); // 3 + 2 = 5

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
            case "<":
              gen.Blt(etiquetaVerdadera);
                break;
               
            default:
                gen.Bgt(etiquetaVerdadera);
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

        
        // Pushear el Resultado
        gen.Push(R.x0); // | 5 |

        gen.PushObjeto(gen.ClonarObjeto(izquierda));

          gen.AddComentario($"--- Fin Relacionales ({Signo}) ---");
        
        return null;

    }

}