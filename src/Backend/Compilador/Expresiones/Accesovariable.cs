using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;

using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Compilador.Expresiones;


public class Accesovariable:Expresion{

    public string Nombre;
    public List <Expresion>? Indices;



public Accesovariable(int linea, int columna, string nombre):base(linea, columna, TipoE.ACCESOVAR){
    
    Nombre = nombre;

}

public Accesovariable(int linea, int columna, string nombre, List < Expresion > indices):base(linea, columna, TipoE.ACCESOVAR){
    
    Nombre = nombre;
    Indices = indices;
}

    public override TipoRetorno Interpretar(GenARM gen)
    {

         gen.AddComentario($"========== Acceso a Variable: {Nombre} ===========");

         var(byteOffset, obj)= gen.GetObjeto(Nombre);
         //se asigna a x0 el valor de byteoffset
         gen.Mov (R.x0, byteOffset);
         // se suma el valor de sp a x0 
         gen.Add (R.x0, R.sp, R.x0);
         //carga en direccion de memoria
         gen.Ldr (R.x0, R.x0);
         //push, para meter a registro x0
         gen.Push(R.x0);

         var ClonObjeto  = gen.ClonarObjeto(obj);
         ClonObjeto.Id = null;
         gen.PushObjeto(ClonObjeto);

          gen.AddComentario("========== Fin Acceso Variable: ===========");


        return null;
           
    }





}