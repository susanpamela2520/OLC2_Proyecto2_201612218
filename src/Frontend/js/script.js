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
	fmt.Println(2)
	fmt.Println(3.1416)
	fmt.Println(true)
	fmt.Println(false)
    fmt.Println("Hola")
    fmt.Println('@')
}`
});

editor.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
	window.addEventListener("resize", function() {
	editor.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
});

CodeMirror.defineMode("arm64", function() {
    return {
        token: function(stream, _) {
            if(stream.match(/\s+|,|\(|\)|\[|\]|!|=/)) {
                return null
            }
            if(stream.match(/\/\/[^\n]*/)) {
                return "comment";
            }
            if(stream.match(/^0x[0-9A-Fa-f]+$/)) {
                return "number";
            }
            if(stream.match(/\#\-?[0-9]+(\.[0-9]+)?/)) {
                return "number";
            }
            if(stream.match(/\.\b(?:global|text|data|word)\b/)) {
                return "builtin";
            }
            if(stream.match(/ecall/)) {
                return "def";
            }
            if(stream.match(/\b(?:svc|cmp|mov(z|k)?|adr|lsl|ldr|str|addi|subi|add|sub|mul|sdiv|udiv)\b/)) {
                return "keyword";
            }
            if(stream.match(/\b(?:b((\.(eq|ne|lt))|le?|r)?|ret)\b/)) {
                return "variable";
            }
            if(stream.match(/\b(?:w([1-3][0-9]|[0-9]|zr|sp)|x([1-3][0-9]|[0-9]|zr)|sp|lr|fp)\b/)) {
                return "attribute";
            }
            if(stream.match(/\"([^\n\"\\]|\\.)*\"/)) {
                return "string";
            }
            if(stream.match(/[a-zA-Z_$][a-zA-Z0-9_$]*:?/)) {
                return null
            }
            stream.next();
            return 'error';
        }
    };
});

var out = CodeMirror(document.getElementById("console"), {
    mode: "arm64",
    lineNumbers: true,
    indentUnit: 4,
    indentWithTabs: true,
    autoCloseBrackets: true,
    matchBrackets: true,
    styleActiveLine: true,
    readOnly: true,
    cursorHeight: 0,
    lineWrapping: false,
    theme: "VSCode"
});

out.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
	window.addEventListener("resize", function() {
	out.setSize(null, window.innerHeight - document.getElementById("editor").offsetTop - 16);
});