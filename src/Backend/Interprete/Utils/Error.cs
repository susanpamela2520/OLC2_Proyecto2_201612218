namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

public class Error:IEquatable<Error>{

    public int Linea;
    public int Columna;

public TipoError Tipo;

public string Descripcion; 


public Error (int linea, int columna, TipoError tipo, string descripcion ){
    Linea = linea;
    Columna = columna;
    Tipo = tipo;
    Descripcion = descripcion;
}

public bool Equals(Error otroError){
    if(otroError == null){
        return false;
    }
    return ToString().Equals(otroError.ToString());
}

    public override int GetHashCode()
    {
        return HashCode.Combine(ToString());
    }



    //Mensaje que se imprime en consola.
    public override string ToString()
    {
        return $"Error {Tipo.GetNombre()}. {Descripcion}. Linea: {Linea}, Columna: {Columna}. ";  //$ incrusta variables en los caracteres 
    }

    public string ObtenerDot(int numero) {
        return $"<tr><td bgcolor=\"white\">{numero}</td><td bgcolor=\"white\">{Tipo.ToString()}</td><td bgcolor=\"white\">{Descripcion}</td><td bgcolor=\"white\">{Linea}</td><td bgcolor=\"white\">{Columna}</td></tr>";
    }

}