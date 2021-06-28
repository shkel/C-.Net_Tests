using System;
using System.Collections.Generic;

namespace CalculatorReversePolishNotation
{
    internal class Operation : Lexeme
    {
        internal enum IntOperation
        {
            Add,
            Substract,
            Multiply,
            Divide
        };
        private static Dictionary<string, IntOperation> validOperations = new() {
            { "+", IntOperation.Add },
            { "-", IntOperation.Substract },
            { "*", IntOperation.Multiply },
            { "/", IntOperation.Divide },
        };
        private readonly IntOperation operand;

        internal Operation(string s)
        {
            if (!validOperations.TryGetValue(s, out this.operand))
            {
                throw new ArgumentException("Wrong number");
            }
        }

        public Int32 Apply(Int32 left, Int32 right)
        {
            return operand switch
            {
                IntOperation.Add => left + right,
                IntOperation.Substract => left - right,
                IntOperation.Multiply => left * right,
                IntOperation.Divide => left / right,
                _ => 0,
            };
        }
    }
}
