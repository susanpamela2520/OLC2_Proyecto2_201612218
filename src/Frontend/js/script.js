var editor = CodeMirror(document.getElementById("editor"), {
    mode: "text/x-go",
    lineNumbers: true,
    styleActiveLine: true,
    indentUnit: 4,
    autoCloseBrackets: true,
    matchBrackets: true,
    caseFold: true,
    theme: "VSCode",
    value:`func main() {
   // 5. Función slices.Index (1 punto)
	fmt.Println("\\n==== Función slices.Index ====")
	puntosIndex := 0

	fmt.Println("Búsqueda de elementos con slices.Index:")
    numeros := []int{10, 20, 30, 40, 50}
	indice1 := slices.Index(numeros, 30)
	indice2 := slices.Index(numeros, 60) // No existe, debería retornar -1
	fmt.Println("Índice de 30:", indice1)
	fmt.Println("Índice de 60:", indice2)

	if indice1 == 2 && indice2 == -1 {
		puntosIndex = puntosIndex + 1
		fmt.Println("OK slices.Index: correcto")
	} else {
		fmt.Println("X slices.Index: incorrecto")
	}

	// 6. Función Strings.Join (1 punto)
	fmt.Println("\\n==== Función Strings.Join ====")
	puntosJoin := 0

	fmt.Println("Unión de strings con strings.Join:")
	palabras := []string{"Hola", "mundo", "desde", "Go"}
	frase := strings.Join(palabras, " ")
	fraseConComas := strings.Join(palabras, ", ")
	fmt.Println("Frase con espacios:", frase)
	fmt.Println("Frase con comas:", fraseConComas)

	if frase == "Hola mundo desde Go" && fraseConComas == "Hola, mundo, desde, Go" {
		puntosJoin = puntosJoin + 1
		fmt.Println("OK strings.Join: correcto")
        
	} else {
		fmt.Println("X strings.Join: incorrecto")
	}
}`
});

editor.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
	window.addEventListener("resize", function() {
	editor.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
});

var out = CodeMirror(document.getElementById("console"), {
    mode: "text",
    lineNumbers: true,
    styleActiveLine: false,
    readOnly: true,
    cursorHeight: 0,
    lineWrapping: false,
    theme: "VSCode"
});

out.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
	window.addEventListener("resize", function() {
	out.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
});