function analyze() {
    fetch(`${path}/parse`,{
        method: 'POST',
        headers,
        body: `{"input":"${getCode()}"}`
    })
    .then(response => response.json())
    .then(response => {
        out.setOption('value',response.salida)
    })
    .catch(error => {})
}
let graphviz
function graphAST() {
    fetch(`${path}/getAST`)
    .then(response => response.json())
    .then(response => {
        graphviz = d3.select('#report').graphviz().scale(0.6).height(document.getElementById('report').clientHeight).width(890*1.9).renderDot(response.salida)
    })
    .catch(error => {console.log(error)})
}
function getSymbolsTable() {
    fetch(`${path}/getTablaSimbolos`)
    .then(response => response.json())
    .then(response => {
        graphviz = d3.select('#report').graphviz().scale(1).height(document.getElementById('report').clientHeight).width(890*1.9).renderDot(response.salida)
    })
    .catch(error => {})
}
function getErrors() {
    fetch(`${path}/getERRORES`)
    .then(response => response.json())
    .then(response => {
        graphviz = d3.select('#report').graphviz().scale(1).height(document.getElementById('report').clientHeight).width(890*1.9).renderDot(response.salida)
    })
    .catch(error => {})
}
function resetGraph() {
    graphviz.resetZoom(d3.transition().duration(500))
}
function getCode() {
    let code = editor.getValue()
    code = code.replace(/\\/g,'\\\\')
    code = code.replace(/\t/g,'    ')
    code = code.replace(/\r?\n|\r/g,'\\n')
    code = code.replace(/"/g,'\\"')
    return code
}