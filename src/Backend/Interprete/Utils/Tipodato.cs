namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

public class Tipodato {

    public Tipo Tipobase;
    public Tipo? Tiposecundario;
    public int Dimensiones;  //vectores y tipo de datos primitivos para ver las dimensiones

    public Tipodato (Tipo tipobase){
        Tipobase = tipobase;
    }
    
    public Tipodato (Tipo tipobase, Tipo? tiposecundario){
        Tipobase = tipobase;
        Tiposecundario  = tiposecundario;
        
    }
public Tipodato (Tipo tipobase, Tipo? tiposecundario, int dimensiones){
        Tipobase = tipobase;
        Tiposecundario  = tiposecundario;
        Dimensiones = dimensiones;
        
    }

public string GetTipo(){

               if(Tipobase == Tipo.SLICE){
                return  string.Concat(Enumerable.Repeat("[]", Dimensiones)) + Tiposecundario?.GetNombre(); }
            if(Tipobase == Tipo.STRUCT){
                
            }
        return Tipobase.GetNombre();

}


}