using System;

namespace BusinessLogic.Exceptions
{
    public class StudentException : ArgumentException
    {
        public StudentException()
        {
        }

        public StudentException(string message)
            : base(message)
        { }
    }
}