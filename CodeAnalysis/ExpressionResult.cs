using System;

namespace CompilerFinal.CodeAnalysis
{
    public class ExpressionResult
    {
        public TipoExpression Type { get; set; }

        public ExpressionResult(TipoExpression tipo)
        {
            Type = tipo;
        }

        public void PrintExpression()
        {
            if (Type != TipoExpression.SyntaxError)
                Console.WriteLine($"\nTipo de expresión: {Type}\n");
            else
                Console.WriteLine($"\n{Type} ERROR: REVISE LA SINTAXIS DE SU EXPRESIÓN\n");

        }
    }
}