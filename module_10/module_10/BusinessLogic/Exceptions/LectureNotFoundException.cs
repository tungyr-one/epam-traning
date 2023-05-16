using System;

namespace BusinessLogic.Exceptions
{
    public class LectureNotFoundException : NotFoundException
    {
        public LectureNotFoundException()
        {
        }

        public LectureNotFoundException(string message)
            : base(message)
        { }
    }
}