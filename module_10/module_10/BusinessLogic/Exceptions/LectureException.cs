using System;

namespace BusinessLogic.Exceptions
{
    public class LectureException : ArgumentException
    {
        public LectureException()
        {
        }

        public LectureException(string message)
            : base(message)
        { }
    }
}