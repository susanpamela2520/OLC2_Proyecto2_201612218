namespace OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils;

public static class Operaciones{

//SUMA
public static Tipo [][] Operaciones1 = {
new Tipo []{Tipo.INT, Tipo.FLOAT, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.FLOAT, Tipo.FLOAT, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.STRING,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL}

};


//RESTA, MULTIPLICACION, DIVISION, %
public static Tipo [][] Operaciones2 = {
new Tipo []{Tipo.INT, Tipo.FLOAT, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.FLOAT, Tipo.FLOAT, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL}
};


//Relacionales != / ==
public static Tipo [][] Operaciones3 = {
new Tipo []{Tipo.BOOL, Tipo.BOOL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.BOOL, Tipo.BOOL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.BOOL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.BOOL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.BOOL}
};

//Relacionales <>  >= <= 
public static Tipo [][] Operaciones4 = {
new Tipo []{Tipo.BOOL, Tipo.BOOL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.BOOL, Tipo.BOOL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.BOOL}
};

//Logicos 
public static Tipo [][] Operaciones5 = {
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.BOOL, Tipo.NIL},
new Tipo []{Tipo.NIL, Tipo.NIL, Tipo.NIL,Tipo.NIL, Tipo.NIL}
};

}