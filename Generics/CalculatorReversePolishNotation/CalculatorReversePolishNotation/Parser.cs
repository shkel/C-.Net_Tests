using System;
using System.Collections.Generic;
namespace CalculatorReversePolishNotation
{
    internal class Parser
    {
        internal static Queue<Lexeme> ParseExpression(string expression)
        {
            var words = expression.Split(" ");
            Queue<Lexeme> lexemes = new Queue<Lexeme>(words.Length);

            foreach (var word in words)
            {
                CalculatorNumber number = CalculatorNumber.TryParse(word);
                if (number != null)
                {
                    lexemes.Enqueue(number);
                }
                else if (lexemes.Count == 0)
                {
                    throw new ArgumentException("Wrong expression");
                }
                else
                {
                    lexemes.Enqueue(new Operation(word));
                }
            }

            return lexemes;
        }

       
    }
}
