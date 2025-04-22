<inicio> :
    (<instruccionglobal> )*;

<instruccionglobal> :
    <funcionMain>  
  | <funcion> ;

<funcionMain> :
   'func'   'main' '('')' <bloque>  ;

<funcion> :
    'func'    Tk_id  '(' <parametros> ')' (<tipoDato>)? <bloque>
     
    ;

<parametros> :
    (
       Tk_id <tipoDato> 
        ( 
            ',' Tk_id <tipoDato> 
            )*)?;

<bloque> :
    '{'(<instruccion> )* '}';

<instruccion> :
    <llamadafunc> ';'?   
    | <println> ';'? 
    | KW_return (<exp> )? ';'? 
    | 'break'   ';'? 
    | 'continue' ';'? 
    | <declaracion> ';'? 
    | <estructuraIf> 
    | <estructuraFor> 
    | <reasignacion> ';'? 
    | <estructuraSwitch>
    ; 

<declaracion> :
    'var' Tk_id <tipoDato> ('='<exp> )? |
    Tk_id ':='<exp>       |
    Tk_id ':=' <tipoDato> <exp>        ;


<reasignacion> :
    Tk_id (
        '=' <exp> | 
        '+=' <exp> | 
        '-=' <exp> |
        '++' | 
        '--' 
    )| 
    Tk_id('['<exp>']')+'=' <exp> ;

<estructuraIf> :
    'if'  <exp>  <bloque> (
        'else' (
            <bloque> |
            <estructuraIf> 
        ) 
    )? ;

<estructuraFor> :
    'for' (<exp>)? <bloque> | 
    'for' Tk_id ':=' <exp> ';' <exp> ';' <reasignacion> <bloque> |
    'for' Tk_id ',' Tk_id ':=' 'range' <exp> <bloque> ; 

<estructuraSwitch> :
    'switch' <exp> '{'<casos>'}';

<casos>  :
    ( 'case' <exp> ':' (<instruccion>)*)*
    ( 'default'':' (<instruccion>)*)?
    ;
    
<println> :
    'fmt.Println'  '('(<exp> (',' <exp> )*)?')' ;

<tipoDato> :
    ('['']' )*  <tipo> ; 

<tipo> :
    'int' | 
    'float64' |
    'string' |
    'rune' |
    'bool';

<exp> :
    '-' <exp>
    |<exp> ('*'| '/' | '%') <exp>
    |<exp> ('+'| '-' ) <exp>
    |<exp> ('<='| '>=' ) <exp>
    |<exp> ('<'| '>' ) <exp>
    |<exp> ('=='| '!=' ) <exp>
    |'!' <exp>
    |<exp> '&&'  <exp> 
    |<exp> '||'  <exp> 
    |'strconv.Atoi' '('<exp>')'
    |'strconv.ParseFloat' '('<exp>')'
    |'reflect.TypeOf' '('<exp>')'
    |'len' '('<exp>')'
    |'slices.Index' '('<exp> ',' <exp>')'
    |'append' '('<exp> ',' <exp>')'
    |'strings.Join' '('<exp> ',' <exp>')'
    | <llamadafunc> 
    | Tk_id ('['<exp>']')+ 
    | Tk_id 
    |Tk_int 
    |Tk_float 
    |Tk_string 
    |TK_rune 
    |'true'
    |'false' 
    |'nil'  
    |<slice>
    | '('<exp>')';

<slice> :
    '{'(<exp> (',' <exp> )*)?'}';


<llamadafunc> :
    Tk_id '('(<exp> (',' <exp> )*)?')'  ; 



'int'        : 'int'         ;
'float64'      : 'float64'     ;
'string'      : 'string'     ;
'bool'     : 'bool'     ;
'rune'      : 'rune'     ;
'fmt.Println'       : 'fmt.Println'     ;
'main'      : 'main'     ;
'func'        : 'func'     ;
'true'      : 'true'     ;
'false'      : 'false'     ;
'nil'       : 'nil'     ;
'break'       : 'break'     ;
'continue'      : 'continue'     ;
'return'    : 'return'     ;
'var'      : 'var'     ;
'if' : 'if';
'for': 'for';
'switch': 'switch';
'else': 'else';
'range': 'range';
'case': 'case';
'default': 'default';
'strconv.Atoi': 'strconv.Atoi';
'strconv.ParseFloat': 'strconv.ParseFloat';
'reflect.TypeOf': 'reflect.TypeOf';
'slices.Index': 'slices.Index';
'strings.Join': 'strings.Join';
'len': 'len';
'append': 'append';