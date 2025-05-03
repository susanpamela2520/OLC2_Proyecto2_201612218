using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;

public class Aritmetico:Expresion{

public Expresion Op1;
public Expresion Op2;

public string Signo;


public Aritmetico (int linea, int columna, Expresion op1, string signo, Expresion op2 )
:base(linea, columna, TipoE.ARITMETICO){

Op1 = op1;
Op2 = op2;
Signo = signo;

}

    public override TipoRetorno Interpretar(GenARM gen)
    {
        switch(Signo) {
            case "+":
                suma(gen);
                break;
            case "-":
                resta(gen);
                break;
            case "*":
                multiplicacion(gen);
                break;
            case "/":
                division(gen);
                break;
            default:
                modulo(gen);
                break;
        };
        return null;
    }

    public TipoRetorno suma(GenARM gen){ // 2 + 3
        gen.AddComentario("----- Aritmetica (+) -----");
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

        // Suma de Enteros
        gen.FAdd(R.d0, R.d1, R.d0); // 3 + 2 = 5

        // Pushear el Resultado
        gen.Push(R.d0); // | 5 |

        gen.PushObjeto(gen.ClonarObjeto(izquierdaFloat? izquierda:derecha));

        gen.AddComentario("--- Fin Aritmetica (+) ---");
        
            return null;
        }

        // Suma de Enteros
        gen.Add(R.x0, R.x0, R.x1); // 3 + 2 = 5

        // Pushear el Resultado
        gen.Push(R.x0); // | 5 |

        gen.PushObjeto(gen.ClonarObjeto(izquierda));

        gen.AddComentario("--- Fin Aritmetica (+) ---");
        return null;
    }

    public TipoRetorno resta(GenARM gen){
        gen.AddComentario("----- Aritmetica (-) -----");
        Op1.Interpretar(gen); // | 2 |
        Op2.Interpretar(gen); // | 3 | 2 |

        // Operando derecho
       var derechaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var derecha = gen.PopObjeto(derechaFloat? R.d0:R.x0); // | 2 |
        var izquierdaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var izquierda = gen.PopObjeto(izquierdaFloat? R.d1:R.x1);

        if(derechaFloat || izquierdaFloat){
            if( !izquierdaFloat ){
                gen.Scvtf(R.d1, R.x1);
            }
            if( !derechaFloat ){
                gen.Scvtf(R.d0, R.x0);
            }

        // Suma de Enteros
        gen.FSub(R.d0, R.d1, R.d0); // 3 + 2 = 5

        // Pushear el Resultado
        gen.Push(R.d0); // | 5 |

        gen.PushObjeto(gen.ClonarObjeto(izquierdaFloat? izquierda:derecha));

        gen.AddComentario("--- Fin Aritmetica (-) ---");
        
            return null;
        }


        // Resta de Enteros
        gen.Sub(R.x0, R.x1, R.x0); // 3 - 2 = 1

        // Pushear el Resultado
        gen.Push(R.x0); // | 1 |

        gen.PushObjeto(gen.ClonarObjeto(izquierda));

        gen.AddComentario("--- Fin Aritmetica (-) ---");
        return null;
    }

    public TipoRetorno multiplicacion(GenARM gen){
        gen.AddComentario("----- Aritmetica (*) -----");
        Op1.Interpretar(gen); // | 2 |
        Op2.Interpretar(gen); // | 3 | 2 |

        // Operando derecho
         var derechaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var derecha = gen.PopObjeto(derechaFloat? R.d0:R.x0); // | 2 |
        var izquierdaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var izquierda = gen.PopObjeto(izquierdaFloat? R.d1:R.x1);

        if(derechaFloat || izquierdaFloat){
            if( !izquierdaFloat ){
                gen.Scvtf(R.d1, R.x1);
            }
            if( !derechaFloat ){
                gen.Scvtf(R.d0, R.x0);
            }

        // Multi de Enteros
        gen.FMul(R.d0, R.d1, R.d0); // 3 + 2 = 5

        // Pushear el Resultado
        gen.Push(R.d0); // | 5 |

        gen.PushObjeto(gen.ClonarObjeto(izquierdaFloat? izquierda:derecha));

        gen.AddComentario("--- Fin Aritmetica (*) ---");
        
            return null;
        }


        // Multiplicación de Enteros
        gen.Mul(R.x0, R.x1, R.x0); // 3 * 2 = 6

        // Pushear el Resultado
        gen.Push(R.x0); // | 6 |

        gen.PushObjeto(gen.ClonarObjeto(izquierda));

        gen.AddComentario("--- Fin Aritmetica (*) ---");
        return null;
    }

    public TipoRetorno division(GenARM gen){
        gen.AddComentario("----- Aritmetica (/) -----");
        Op1.Interpretar(gen); // | 2 |
        Op2.Interpretar(gen); // | 3 | 2 |

        // Operando derecho
        var derechaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var derecha = gen.PopObjeto(derechaFloat? R.d0:R.x0); // | 2 |
        var izquierdaFloat = gen.TopePila().Type == Tipo.FLOAT;
        var izquierda = gen.PopObjeto(izquierdaFloat? R.d1:R.x1);
        
        if(derechaFloat || izquierdaFloat){
            if( !izquierdaFloat ){
                gen.Scvtf(R.d1, R.x1);
            }
            if( !derechaFloat ){
                gen.Scvtf(R.d0, R.x0);
            }

        // Suma de Enteros
        gen.FDiv(R.d0, R.d1, R.d0); // 3 + 2 = 5

        // Pushear el Resultado
        gen.Push(R.d0); // | 5 |

        gen.PushObjeto(gen.ClonarObjeto(izquierdaFloat? izquierda:derecha));

        gen.AddComentario("--- Fin Aritmetica (/) ---");
        
            return null;
        }


        // División de Enteros
        gen.Div(R.x0, R.x1, R.x0); // 3 / 2 = 1

        // Pushear el Resultado
        gen.Push(R.x0); // | 1 |

        gen.PushObjeto(gen.ClonarObjeto(izquierda));

        gen.AddComentario("--- Fin Aritmetica (/) ---");
        return null;
    }

    public TipoRetorno modulo(GenARM gen){
        gen.AddComentario("----- Aritmetica (%) -----");
        Op1.Interpretar(gen); // | 2 |
        Op2.Interpretar(gen); // | 3 | 2 |

        // Operando derecho
        var derecha = gen.PopObjeto(R.x1); // | 2 |
        var izquierda = gen.PopObjeto(R.x0); // | |

        // División de Enteros 3 % 2 = 1
        gen.Div(R.x2, R.x0, R.x1); // 3 / 2 = 1
        gen.Mul(R.x2, R.x2, R.x1); // 1 * 2 = 2
        gen.Sub(R.x0, R.x0, R.x2); // 3 - 2 = 1

        // Pushear el Resultado
        gen.Push(R.x0); // | 1 |

        gen.PushObjeto(gen.ClonarObjeto(izquierda));

        gen.AddComentario("--- Fin Aritmetica (%) ---");
        return null;
    }
}