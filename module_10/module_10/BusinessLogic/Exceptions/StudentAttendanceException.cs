using System;

namespace BusinessLogic.Exceptions
{
    public class StudentAttendanceException : ArgumentException
    {
        public StudentAttendanceException()
        {
        }

        public StudentAttendanceException(string message)
            : base(message)
        { }
    }
}