using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

public static class Consola{

public static List < string > Impresiones = new ();
public static List < Error > Errores = new ();

public static List <string []> TablaSimbolos = new(); 

public static void EliminarDuplicados(){
    Errores = Errores.Distinct().ToList();
}

public static string ObtenerSimbolos(){
    TablaSimbolos = TablaSimbolos.Distinct().ToList();
    string Simbolos = " ";
    for(int i = 0; i < TablaSimbolos.Count; i++){
        //  s.Id, s.getType(s.Type), s.NameEnv, s.Line, s.Column
        Simbolos += $"<tr><td bgcolor=\"white\">{i}</td><td bgcolor=\"white\">{TablaSimbolos[i][1]}</td><td bgcolor=\"white\">{TablaSimbolos[i][2]}</td><td bgcolor=\"white\">{TablaSimbolos[i][3]}</td><td bgcolor=\"white\">{TablaSimbolos[i][0]}</td><td bgcolor=\"white\">{TablaSimbolos[i][4]}</td><td bgcolor=\"white\">{TablaSimbolos[i][5]}</td></tr>";
    }
    return Simbolos;
}
public static string Salidas(){

string Salidas = string.Join("", Impresiones);

if (Errores.Count > 0){
    EliminarDuplicados();
    if(!string.IsNullOrEmpty(Salidas)){
        Salidas += (Salidas[^1] != '\n'?"\n":"")+"\nErrores\n"; //Valua el ultimo caracter, que no tenga salto de linea
    }else{
        Salidas += "Errores\n";
    }

    Salidas += string.Join ("\n", Errores.Select(e => e.ToString()));
}
 return Salidas;

}

public static void Limpiar(){

Impresiones.Clear();
Errores.Clear();
TablaSimbolos.Clear();

}

}