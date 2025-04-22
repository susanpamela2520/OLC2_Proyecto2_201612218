using Microsoft.AspNetCore.Mvc;
using OLC2_Proyecto1_201612218.src.Backend.controlador;
using OLC2_Proyecto1_201612218.src.Backend.parser;
using Antlr4.Runtime;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Entorno1;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Instrucciones;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Abstracts;
using OLC2_Proyecto1_201612218.src.Backend.Interprete.Utils;


var builder = WebApplication.CreateBuilder(args);  //se cera laapi

builder.Services.AddCors(options => {  //cores ayudan a realizar trafico de datros entre front y back
    options.AddPolicy("AllowAnyOrigin", politica => {   //Define politica desde cualquier origen
        politica.AllowAnyOrigin() // Cualquier origen
            .AllowAnyMethod() // Cualquier Método: GET, PUT, POST, DELETE
            .AllowAnyHeader(); // Cualquier encabazado
    });
});

builder.Services.AddControllers(); // Registra los contenedores de dependencias de .NET

var app = builder.Build();

app.UseCors("AllowAnyOrigin"); // Usar CORS

app.UseHttpsRedirection(); // Redireccionamiento HTTP

app.UseAuthorization(); // Autorización en el middleware de .NET

// Endpoints

app.MapGet("/", () => "OLC2_Proyecto1_201612218");
var controlador = new Controlador();

var ast = "";

app.MapPost("/parse", async ([FromBody]Request req) => {
        await Task.Delay(100);

        Consola.Limpiar();
        try {
            AntlrInputStream inputStream = new(req.Input);
            ParserLexer lexer = new(inputStream);
            lexer.AddErrorListener(new LexerErrorListener());
            CommonTokenStream cts = new(lexer);
            ParserParser parser = new(cts);
            parser.AddErrorListener(new SyntaxErrorListener());
            ParserParser.InicioContext res = parser.inicio();
            ast = controlador.DotTree(res, parser.RuleNames);

            //cracion de entorno global

            Entorno global = new (null, "Global");
            
            //Funcion donde se alamcenara la funcion main
            
            Funcion? main = null;

            foreach(Instruccion instruccion in res.resultado){
                if(instruccion.Tipoi == TipoI.FUNCION){
                        if(((Funcion) instruccion).Nombre == "main"){
                            main = (Funcion) instruccion;
                        }else{
                            instruccion.Interpretar(global);
                       }
                }else{
                    instruccion.Interpretar(global);
                }

            }

            if(main != null){
                main.Interpretar(global);
            }

            string salidas = Consola.Salidas();

            return Results.Ok(new { status = 200, salida = salidas});

        }

        catch(Exception e){
            Console.Error.WriteLine(e);
            return Results.Ok(new { status = 500, salida = e.ToString() });
        }

        
    });

app.MapPost("/test", async ([FromBody]Request req) => {
    await Task.Delay(100);
        
        
        try {

            string contenido = "";
            using (StreamReader sr = new (req.Input??""))
            {
                contenido = sr.ReadToEnd();
            }

            AntlrInputStream inputStream = new(contenido);
            ParserLexer lexer = new(inputStream);
            lexer.AddErrorListener(new LexerErrorListener());
            CommonTokenStream cts = new(lexer);
            ParserParser parser = new(cts);
            parser.AddErrorListener(new SyntaxErrorListener());
            ParserParser.InicioContext res = parser.inicio();
            Console.WriteLine(controlador.DotTree(res, parser.RuleNames));
            return Results.Ok(new { status = 200, salida = "" });

        }

        catch(Exception e){
            Console.Error.WriteLine(e);
            return Results.Ok(new { status = 500, salida = e.ToString() });
        }

        
    });

app.MapGet("/getAST", async () => {
    await Task.Delay(100);
        
        
        try {

           
            return Results.Ok(new { status = 200, salida = ast});

        }

        catch(Exception e){
            Console.Error.WriteLine(e);
            return Results.Ok(new { status = 500, salida = e.ToString() });
        }

        
    });

app.MapGet("/getERRORES", async () => {
    await Task.Delay(100);
        
        
        try {

            string dot = "digraph Errors {graph[fontname=\"Arial\" labelloc=\"t\" bgcolor=\"#252526\" fontcolor=\"white\"];node[shape=none fontname=\"Arial\"];label=\"Errores\";table[label=<<table border=\"0\" cellborder=\"1\" cellspacing=\"0\" cellpadding=\"3\"><tr><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">No.</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Tipo</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Descripción</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Línea</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Columna</font></td></tr>";
            // Consola.EliminarDuplicados();
            int i = 1;
            foreach (var item in Consola.Errores)
            {
                dot+= item.ObtenerDot(i);
                i++;
            }
            dot += "</table>>];}";
            return Results.Ok(new { status = 200, salida = dot});

        }

        catch(Exception e){
            Console.Error.WriteLine(e);
            return Results.Ok(new { status = 500, salida = e.ToString() });
        }

        
    });

    
app.MapGet("/getTablaSimbolos", async () => {
    await Task.Delay(100);
        
        
        try {

            string dot = "digraph SymbolsTable {graph[fontname=\"Arial\" labelloc=\"t\" bgcolor=\"#252526\" fontcolor=\"white\"];node[shape=none fontname=\"Arial\"];label=\"Tabla de Símbolos\";table[label=<<table border=\"0\" cellborder=\"1\" cellspacing=\"0\" cellpadding=\"3\"><tr><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">No.</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Identificador</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Tipo</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Tipo de Dato</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Entorno</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Línea</font></td><td bgcolor=\"#009900\" width=\"100\"><font color=\"#FFFFFF\">Columna</font></td></tr>";
            
            dot+= Consola.ObtenerSimbolos();

            dot += "</table>>];}";

            Console.WriteLine(dot);
            return Results.Ok(new { status = 200, salida = dot});

        }

        catch(Exception e){
            Console.Error.WriteLine(e);
            return Results.Ok(new { status = 500, salida = e.ToString() });
        }

        
    });



app.Run();