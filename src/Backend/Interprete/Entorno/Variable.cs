using System.Reflection.Metadata.Ecma335;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;

public class Variable{

    public TipoRetorno Valor;
    public string Nombre;

    public Tipodato Tipo;

    public Variable(TipoRetorno valor, string nombre, Tipodato tipo){

            Valor = valor;
            Nombre = nombre;
            Tipo = tipo;

    }

    public bool Equals(Error otraVariable){
        if(otraVariable == null){
            return false;
        }
        return ToString().Equals(otraVariable.GetHashCode());
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Valor.ToString(), Nombre, Tipo.ToString());
    }
}