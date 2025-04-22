

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;


namespace OLC2_Proyecto2_201612218.src.Backend.controlador;

public class Controlador {
    //Comenzar a graficar desde el nodo raiz.
    public string DotTree(RuleContext tree, string[] rulenames) {
        string dot = "digraph Tree {";
        dot += "\n\tgraph[labelloc=\"t\"];";
        dot += "\n\tnode[fontname=\"Arial\" fontsize=\"8\" width=\"0\" height=\"0\"];";
        dot += "\n\tedge[dir=none];";
        dot += DotTree("i", tree, rulenames);
        dot += "\n}";
        return dot;
    }

    //recorre el arbol sintactico de forma recursiva
    public string DotTree(string id, RuleContext tree, string[] ruleNames) {
        string s = ruleNames[tree.RuleIndex];
        string res = $"\n\tn{id}[label=\"{s.Replace("\"", "\\\"")}\"];";
        for(int i = 0; i < tree.ChildCount; i++) {
            IParseTree child = tree.GetChild(i);
            if(child is RuleContext) {
                res += DotTree($"{id}{i}", (RuleContext) tree.GetChild(i), ruleNames);
            } else {
                res += $"\n\tn{id}{i}[label=\"{child.ToString().Replace("\"", "\\\"")}\"];";
            }
            res += $"\n\tn{id} -> n{id}{i};";
        }
        return res;
    }
}
