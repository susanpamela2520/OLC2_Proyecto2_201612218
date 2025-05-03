using Antlr4.Runtime;
using Microsoft.AspNetCore.Mvc;
using OLC2_Proyecto2_201612218.src.Backend.controlador;
using OLC2_Proyecto2_201612218.src.Backend.parser;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Instrucciones;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Abstracts;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Utils;
using OLC2_Proyecto2_201612218.src.Backend.Compilador.Generador;
using OLC2_Proyecto2_201612218.src.Backend.Interprete.Entorno1;
using FuncionInterprete = OLC2_Proyecto2_201612218.src.Backend.Interprete.Instrucciones.Funcion;
using InstruccionInterprete = OLC2_Proyecto2_201612218.src.Backend.Interprete.Abstracts.Instruccion;
using TipoIInterprete = OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils.TipoI;
using ConsolaInterprete = OLC2_Proyecto2_201612218.src.Backend.Interprete.Utils.Consola;
using ParserLexerInterprete = OLC2_Proyecto2_201612218.src.Backend.parser_Interprete.ParserLexer;
using ParserParserInterprete = OLC2_Proyecto2_201612218.src.Backend.parser_Interprete.ParserParser;
using LexerErrorListenerInterprete = OLC2_Proyecto2_201612218.src.Backend.parser_Interprete.LexerErrorListener;
using SyntaxErrorListenerInterprete = OLC2_Proyecto2_201612218.src.Backend.parser_Interprete.SyntaxErrorListener;


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

app.MapGet("/", () => "OLC2_Proyecto2_201612218");
var controlador = new Controlador();

var ast = "";

app.MapPost("/parse", async ([FromBody]Request req) => {
        await Task.Delay(100);

        Consola.Limpiar();
        try {
            AntlrInputStream inputStream = new(req.Input);
            ParserLexerInterprete lexer = new(inputStream);
            lexer.AddErrorListener(new LexerErrorListenerInterprete());
            CommonTokenStream cts = new(lexer);
            ParserParserInterprete parser = new(cts);
            parser.AddErrorListener(new SyntaxErrorListenerInterprete());
            ParserParserInterprete.InicioContext res = parser.inicio();
            ast = controlador.DotTree(res, parser.RuleNames);

            //cracion de entorno global
            Entorno global = new (null, "Global");

            //Funcion donde se alamcenara la funcion main
            
            FuncionInterprete? main = null;

            foreach(InstruccionInterprete instruccion in res.resultado){
                if(instruccion.Tipoi == TipoIInterprete.FUNCION){
                        if(((FuncionInterprete) instruccion).Nombre == "main"){
                            main = (FuncionInterprete) instruccion;
                        }else{
                            instruccion.Interpretar(global);
                        }
                }else{
                    instruccion.Interpretar(global);
                }
            }

            // if(main != null){
            //     main.Interpretar(global);
            // }

            if(!ConsolaInterprete.Salidas().Equals("")) {
                return Results.Ok(new { status = 200, salida = ConsolaInterprete.Salidas()});
            }

            inputStream = new(req.Input);
            ParserLexer lexerC = new(inputStream);
            lexerC.AddErrorListener(new LexerErrorListener());
            cts = new(lexerC);
            ParserParser parserC = new(cts);
            parserC.AddErrorListener(new SyntaxErrorListener());
            ParserParser.InicioContext resC = parserC.inicio();

            //Generador de ARM
            GenARM gen = new ();

            Funcion? main1 = null;
            foreach(Instruccion instruccion in resC.resultado){
                if(((Funcion) instruccion).Nombre == "main"){
                    main1 = (Funcion) instruccion;
                }else{
                    instruccion.Interpretar(gen);
                }
            }

            if(main1 != null){
                main1.Interpretar(gen);
            }

            gen.generarCodigo();

            //genera el codigo de una vez en ensamblador 
            StreamWriter sw = new StreamWriter("./Out/program.s");
            sw.Write(gen.getCodigo());
            sw.Close();

            return Results.Ok(new { status = 200, salida = gen.getCodigo()});

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