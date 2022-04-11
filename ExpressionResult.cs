using System;

namespace CompilerFinal
{
    public class ExpressionResult
    {
        public TipoExpression Type { get; set; }

        public ExpressionResult(TipoExpression tipo)
        {
            Type = tipo;
        }

        public void PrintExpression() => Console.WriteLine($"Tipo de expresión: {Type}");
    }
}