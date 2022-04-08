using System;

namespace BLL
{
    public class TrainingException : Exception
    {
        public TrainingException(string message) : base(message)
        {
        }

        public TrainingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
