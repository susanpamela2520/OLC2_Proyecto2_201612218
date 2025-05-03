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
	fmt.Println("a" == "a")
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
            if(stream.match(/\#\-?(0x)?[0-9A-Fa-f]+|[0-9]+/)) {
                return "number";
            }
            if(stream.match(/\.\b(?:global|text|data|word|ascii|space)\b/)) {
                return "builtin";
            }
            if(stream.match(/ecall/)) {
                return "def";
            }
            if(stream.match(/\b(?:cbn?z|svc|scvtf|f(rint(m|n)|cvtzs)|cmp|tst|f?mov(z|k)?|adr|l(d(rs?b?|p)|sl)|st(rb?|p)|f?(addi?|m?subi?|mul|(s|u)?div|neg))\b/)) {
                return "keyword";
            }
            if(stream.match(/\b(?:b(eq|ne|l(e|t)?|g(e|t)|r)?|ret)\b/)) {
                return "variable";
            }
            if(stream.match(/\b(?:(x|d|w|v)([0-9]|[1-2][0-9]|3[0-1]|zr)|sp|lr|fp|pc)\b/)) {
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