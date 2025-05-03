using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;


public class Declaracion:Instruccion{

    public string Nombre;
    public Tipodato? Tipo;

    public Expresion? Valor;


    public Declaracion(int linea, int columna, string nombre, Tipodato tipo, Expresion valor):base(linea, columna, TipoI.DECLARACION){

        Nombre = nombre;
        Tipo = tipo;
        Valor = valor;


    }

    public Declaracion(int linea, int columna, string nombre, Tipodato tipo):base(linea, columna, TipoI.DECLARACION ){

        Nombre = nombre;
        Tipo = tipo;
       
    }


    public Declaracion(int linea, int columna, string nombre, Expresion valor):base(linea, columna, TipoI.DECLARACION ){

        Nombre = nombre;
        Valor = valor;

    }


    public override TipoRetorno? Interpretar(GenARM gen) {
        if(gen.Frame == null) {
            gen.AddComentario($"========== Declaración: {Nombre} ===========");
            if(Tipo!= null){
                if(Valor != null){
                    Valor.Interpretar(gen);
                
                }else{
                    switch(Tipo.Tipobase){
                        case Utils.Tipo.INT : new Primitivo(0,0, 0, Tipo.Tipobase).Interpretar(gen); break;
                        case Utils.Tipo.FLOAT : new Primitivo(0,0, 0.0, Tipo.Tipobase).Interpretar(gen); break;
                        case Utils.Tipo.BOOL : new Primitivo(0,0, "false", Tipo.Tipobase).Interpretar(gen); break;
                        case Utils.Tipo.STRING : new Primitivo(0,0, "", Tipo.Tipobase).Interpretar(gen); break;
                        default : new Primitivo(0,0, '\0', Tipo.Tipobase).Interpretar(gen); break;
                    }

                }

            }else{
                Valor.Interpretar(gen);

            }

            if(gen.EstaEnFuncion != null) {
                var objetoLocal = gen.GetFrameLocal(gen.fpOffset);
                var valorObjeto = gen.PopObjeto(R.x0);

                gen.Mov(R.x1, objetoLocal.Offset * 8);
                gen.Sub(R.x1, R.x29, R.x1);
                gen.Str(R.x0, R.x1);

                objetoLocal.Type = valorObjeto.Type;
                gen.fpOffset ++;
            } else {
                gen.NombrarObjeto(Nombre); // se hizo una declaración (Se etiqueto el objeto en pila)
            }

            gen.AddComentario("========== Fin Declaración ===========");
        } else {
            gen.Frame.FrameElements.Add(new FrameElement(Nombre, gen.Frame.getOffsetTotal()));
            gen.Frame.LocalOffset += 1;
        }
        return null; 

    }
}