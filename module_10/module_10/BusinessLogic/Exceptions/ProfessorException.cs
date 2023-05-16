using System;

namespace BusinessLogic.Exceptions
{
    public class ProfessorException : ArgumentException
    {
        public ProfessorException()
        {
        }

        public ProfessorException(string message)
            : base(message)
        { }
    }
}