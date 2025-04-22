grammar Parser;

@header{
    namespace OLC2_Proyecto1_201612218.src.Backend.parser;
     using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;
     using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
     using OLC2_Proyecto1_201612218.src.Backend.Interprete.Expresiones;
     using OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;
     using MiSwitch = OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones.Switch;
     
}

options{
    language = CSharp;
}

//ANALISIS SINTACTICO
//Producciones

inicio returns [List < Instruccion > resultado] locals [ List < Instruccion > L = new()]:
    (ins=instruccionglobal {$L.Add ($ins.resultado);})*{$resultado=$L;};

instruccionglobal returns [ Instruccion resultado  ]:
    f1=funcionMain  {$resultado = $f1.resultado;}
  | f2=funcion {$resultado = $f2.resultado;};

funcionMain returns [ Instruccion resultado ]:
   f = KW_func KW_main '('')' b=bloque {
        $resultado = new Funcion ($f.line, $f.pos, "main", new List  < Parametro >(), $b.resultado, new Tipodato (Tipo.NIL));
    } ;

funcion returns [ Instruccion resultado ] locals [ Tipodato t = new Tipodato(Tipo.NIL) ]:
    f = KW_func  id = Tk_id  '(' p = parametros ')' (T1= tipoDato{$t = $T1.resultado;})? b = bloque
    {
        $resultado = new Funcion ($f.line, $f.pos, $id.text, $p.resultado, $b.resultado, $t);
    } 
    ;

parametros returns [List < Parametro > resultado] locals [ List < Parametro > params = new()]:
    (
       id = Tk_id t = tipoDato {$params.Add(new Parametro($id.line , $id.pos, $id.text, $t.resultado));}
        ( 
            ',' id = Tk_id t=tipoDato {$params.Add(new Parametro($id.line , $id.pos, $id.text, $t.resultado));}
            )*)?{
                $resultado = $params;
                
            };

bloque returns [ Instruccion resultado ] locals [ List <Statement>  a = new ()]:
    B='{'(i=instruccion {$a.Add ($i.resultado);})* '}'{$resultado = new Bloque ($B.line, $B.pos, $a);};

instruccion returns [ Statement resultado ] locals [ Expresion? retorno = null]:
    l= llamadafunc ';'?   {$resultado = $l.resultado;}
    | p=println ';'? {$resultado = $p.resultado;}
    | r=KW_return (e=exp {$retorno  = $e.resultado;})? ';'? {$resultado = new Return($r.line, $r.pos, $retorno);}
    | b=KW_break  ';'? {$resultado = new Break($b.line, $b.pos);}
    | c=KW_continue ';'? {$resultado = new Continue($c.line, $c.pos);}
    | d=declaracion ';'? {$resultado = $d.resultado;}
    | i=estructuraIf {$resultado = $i.resultado;}
    | f=estructuraFor {$resultado = $f.resultado;}
    | z=reasignacion ';'? {$resultado = $z.resultado;}
    | s=estructuraSwitch{$resultado = $s.resultado;}
    ; 

declaracion returns [ Instruccion resultado] locals [Expresion? val=null]:
    v=KW_var id=Tk_id t=tipoDato ('='e=exp {$val = $e.resultado;})? {$resultado = new Declaracion($v.line, $v.pos, $id.text, $t.resultado, $val);}|
    id=Tk_id ':='e=exp       {$resultado = new Declaracion($id.line, $id.pos, $id.text, null, $e.resultado);}|
    id=Tk_id ':=' t=tipoDato e=exp        {$resultado = new Declaracion($id.line, $id.pos, $id.text, $t.resultado, $e.resultado);};


reasignacion returns [ Instruccion resultado] locals [ Instruccion? val=null, List <Expresion> i = new()]:
    id=Tk_id (
        '=' e=exp {$val = new Asignacion($id.line, $id.pos, $id.text, $e.resultado);}| 
        '+=' e=exp {$val = new Asignacion($id.line, $id.pos, $id.text, new Aritmetico(0,0,new Accesovariable(0,0, $id.text), "+", $e.resultado));}| 
        '-=' e=exp {$val = new Asignacion($id.line, $id.pos, $id.text, new Aritmetico(0,0,new Accesovariable(0,0, $id.text), "-", $e.resultado));}|
        '++' {$val = new Asignacion($id.line, $id.pos, $id.text, new Aritmetico(0,0,new Accesovariable(0,0, $id.text), "+", new Primitivo (0,0, "1", Tipo.INT)));}| 
        '--' {$val = new Asignacion($id.line, $id.pos, $id.text, new Aritmetico(0,0,new Accesovariable(0,0, $id.text), "-", new Primitivo (0,0, "1", Tipo.INT)));}
    ){$resultado = $val;}| 
    id=Tk_id('['e1=exp']'{$i.Add ($e1.resultado);})+'=' e2=exp {$resultado = new Asignacion ($id.line, $id.pos, $id.text, $e2.resultado, $i);};

estructuraIf returns [ Instruccion resultado ] locals [Instruccion? b= null]:
    i= KW_if  e=exp  b1=bloque (
        KW_else (
            b2=bloque {$b = $b2.resultado;}|
            b3=estructuraIf {$b = $b3.resultado;}
        ) 
    )? {$resultado = new If ($i.line, $i.pos, $e.resultado, $b1.resultado, $b);};

estructuraFor returns [Instruccion resultado ] locals [ Expresion condicion = new Primitivo (0,0, "true", Tipo.BOOL)]:
    f1= KW_for (e1=exp{$condicion = $e1.resultado;})? b1=bloque {$resultado = new While($f1.line, $f1.pos, $condicion, $b1.resultado);}| 
    f2=KW_for id1=Tk_id ':=' e2=exp ';' e3=exp ';' r=reasignacion b2=bloque {$resultado = new For($f2.line, $f2.pos, 
    new Declaracion($id1.line, $id1.pos, $id1.text, null, $e2.resultado),$e3.resultado, 
    $r.resultado, $b2.resultado) ;}|
    f3=KW_for id3=Tk_id ',' id4=Tk_id ':=' KW_range e4=exp b3=bloque {$resultado = new ForEach($f3.line, $f3.pos, $id3.text, $id4.text, $e4.resultado, $b3.resultado);}; 

estructuraSwitch returns [ Instruccion resultado]:
    sw= KW_switch e=exp '{'c=casos'}'{$resultado = new MiSwitch($sw.line, $sw.pos, $e.resultado, $c.resultado);};

casos returns [  List <Case>  resultado = new () ] locals [ List <Statement>  a = new () ] :
    ({$a=new();} c=KW_case e=exp ':' (i1=instruccion{$a.Add($i1.resultado);})*{$resultado.Add(new Case($c.line, $c.pos, $e.resultado, new Bloque(0,0,$a)));})*
    ({$a=new();} d=KW_default':' (i2=instruccion{$a.Add($i2.resultado);})*{$resultado.Add(new Case($d.line, $d.pos, null , new Bloque(0,0,$a)));})?
    ;
    
println returns [ Instruccion resultado ] locals [ List <Expresion>  a = new ()]:
    p= KW_println '('(e=exp {$a.Add ($e.resultado);}(',' e=exp {$a.Add ($e.resultado);})*)?')' {$resultado = new Print($p.line, $p.pos, $a);};

tipoDato returns [ Tipodato resultado ] locals [ int  dimensiones = 0 ]:
    ('['']' {$dimensiones ++;})* t =  tipo {
        if($dimensiones == 0){
            $resultado = new Tipodato($t.resultado);
            }else { 
                $resultado = new Tipodato(Tipo.SLICE, $t.resultado, $dimensiones);
            }
    }; 

tipo returns [ Tipo resultado ]:
    KW_int {$resultado = Tipo.INT;}| 
    KW_float {$resultado = Tipo.FLOAT;}|
    KW_string {$resultado = Tipo.STRING;}|
    KW_rune {$resultado = Tipo.RUNE;}|
    KW_bool {$resultado = Tipo.BOOL;};

exp returns [ Expresion resultado ] locals [List < Expresion > i = new()]:
    s='-' e1=exp{$resultado = new Aritmetico ($s.line, $s.pos, new Primitivo (0,0,0,Tipo.INT), $s.text, $e1.resultado);}
    |e1=exp s=('*'| '/' | '%') e2=exp{$resultado = new Aritmetico ($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |e1=exp s=('+'| '-' ) e2=exp{$resultado = new Aritmetico ($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |e1=exp s=('<='| '>=' ) e2=exp{$resultado = new Relacional($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |e1=exp s=('<'| '>' ) e2=exp{$resultado = new Relacional($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |e1=exp s=('=='| '!=' ) e2=exp{$resultado = new Relacional($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |s='!' e1=exp{$resultado = new Logico($s.line, $s.pos, $s.text, $e1.resultado);}
    |e1=exp s='&&'  e2=exp {$resultado = new Logico($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |e1=exp s='||'  e2=exp {$resultado = new Logico($e1.resultado.Linea, $e1.resultado.Columna, $e1.resultado, $s.text, $e2.resultado);}
    |f1=KW_atoi '('e=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e.resultado);}
    |f1=KW_parsefloat '('e=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e.resultado);}
    |f1=KW_typeof '('e=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e.resultado);}
    |f1=KW_len '('e=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e.resultado);}
    |f1=KW_index '('e1=exp ',' e2=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e1.resultado, $e2.resultado);}
    |f1=KW_append '('e1=exp ',' e2=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e1.resultado, $e2.resultado);}
    |f1=KW_join '('e1=exp ',' e2=exp')'{$resultado= new Embebidas($f1.line, $f1.pos, $f1.text,$e1.resultado, $e2.resultado);}
    | f=llamadafunc {$resultado = $f.resultado;}
    | id = Tk_id ('['e=exp']'{$i.Add($e.resultado);})+{$resultado = new Accesovariable($id.line, $id.pos, $id.text, $i);} 
    | id = Tk_id{$resultado = new Accesovariable($id.line, $id.pos, $id.text);} 
    |p= Tk_int {$resultado= new Primitivo($p.line, $p.pos, $p.text, Tipo.INT);}
    |p= Tk_float {$resultado= new Primitivo($p.line, $p.pos, $p.text, Tipo.FLOAT);}
    |p= Tk_string {$resultado= new Primitivo($p.line, $p.pos, $p.text.Substring(1,$p.text.Length-2), Tipo.STRING);}
    |p= TK_rune {$resultado= new Primitivo($p.line, $p.pos, $p.text.Substring(1,$p.text.Length-2), Tipo.RUNE);}
    |p= KW_true{$resultado= new Primitivo($p.line, $p.pos, $p.text, Tipo.BOOL);}
    |p= KW_false{$resultado= new Primitivo($p.line, $p.pos, $p.text, Tipo.BOOL);}
    |p= KW_nil {$resultado= new Primitivo($p.line, $p.pos, $p.text, Tipo.NIL);}
    |v=slice{$resultado = $v.resultado;}
    | '('e=exp')'{$resultado = $e.resultado;};

slice returns [ Expresion resultado] locals [ List <Expresion>  a = new ()]:
    l='{'(e=exp {$a.Add ($e.resultado);}(',' e=exp {$a.Add ($e.resultado);})*)?'}'{$resultado = new Slice($l.line, $l.pos, $a);};


llamadafunc returns [ Expresion resultado ] locals [ List <Expresion>  a = new ()]:
    id= Tk_id '('(e=exp {$a.Add ($e.resultado);}(',' e=exp {$a.Add ($e.resultado);})*)?')' {$resultado=new Llamadafunc ($id.line , $id.pos, $id.text, $a);} ; 

//ANALISIS LEXICO

//Macros - Fragmentos

fragment UNUSED   : [ \r\t]+                            ;  //caracteres que se ignora
fragment CONTENT  : (~('\n'|'"'|'\\')|'\\'.)            ;  //cualquier tipo de caracter excepto saltos de linea
fragment ID       : ('_')*[a-zA-Z][a-zA-Z0-9_]*         ;    //cualquier identificador
fragment RUNE     : '\''(CONTENT)'\''                     ;    //valida los bites (character llamado rune)
fragment STRING   : '"'(CONTENT)*'"'                    ;    //Strings o cadenas
fragment INTEGER  : [0-9]+                              ;    //numeros enteros
fragment FLOAT    : [0-9]+'.'[0-9]+                     ;   //numeros con puntos decimal
fragment COMMENTS : '//'(~[\r\n])*                      ;   //comentarios simples o de una sola linea
fragment COMMENTM : [/][*]~[*]*[*]+(~[*/]~[*]*[*]+)*[/] ;   //comentarios multilinea o bloques de comentarios

//Tokens

//Reservadas 

KW_int        : 'int'         ;
KW_float      : 'float64'     ;
KW_string      : 'string'     ;
KW_bool      : 'bool'     ;
KW_rune      : 'rune'     ;
KW_println      : 'fmt.Println'     ;
KW_main      : 'main'     ;
KW_func      : 'func'     ;
KW_true      : 'true'     ;
KW_false     : 'false'     ;
KW_nil      : 'nil'     ;
KW_break      : 'break'     ;
KW_continue      : 'continue'     ;
KW_return      : 'return'     ;
KW_var      : 'var'     ;
KW_if : 'if';
KW_for: 'for';
KW_switch: 'switch';
KW_else: 'else';
KW_range: 'range';
KW_case: 'case';
KW_default: 'default';
KW_atoi: 'strconv.Atoi';
KW_parsefloat: 'strconv.ParseFloat';
KW_typeof: 'reflect.TypeOf';
KW_index: 'slices.Index';
KW_join: 'strings.Join';
KW_len: 'len';
KW_append: 'append';



//Valores 

TK_rune : RUNE;
Tk_string: STRING;
Tk_int: INTEGER;
Tk_float: FLOAT;

//Identificador
Tk_id: ID;

//Ignorados
IGNORE:UNUSED -> skip;
SALTO:'\n'  -> skip;
COMENTARIOS: (COMMENTS | COMMENTM)-> skip;