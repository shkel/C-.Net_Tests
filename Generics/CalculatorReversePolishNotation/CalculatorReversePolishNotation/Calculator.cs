using System;
using System.Collections.Generic;

namespace CalculatorReversePolishNotation
{
    public class Calculator
    {

        public static int Calculate(string expression)
        {
            if (expression == null) throw new ArgumentNullException("Expression is not defined");
            var trimmedString = expression.Trim();
            if (trimmedString.Length == 0) return 0;
            Queue<Lexeme> lexemes = Parser.ParseExpression(trimmedString);
            return CalculateExpression(lexemes);
        }

        private static int CalculateExpression(Queue<Lexeme> lexemes)
        {
            Stack<Int32> values = new(lexemes.Count);
            foreach (var lexeme in lexemes)
            {
                if (lexeme is CalculatorNumber n)
                {
                    values.Push(n.Num);
                }
                else if (lexeme is Operation op)
                {
                    if (values.Count > 1)
                    {
                        int secondValue = values.Pop();
                        int firstValue = values.Pop();
                        values.Push(op.Apply(firstValue, secondValue));
                    }
                    else
                    {
                        throw new ArgumentException("Not enough numbers");
                    }
                }
                else
                {
                    throw new ArgumentException("Unexpected lexeme");
                }
            }
            if (values.Count > 1)
            {
                throw new ArgumentException("Invalid expression");
            }
            return values.Count == 0 ? 0 : values.Pop();
        }
    }
}
