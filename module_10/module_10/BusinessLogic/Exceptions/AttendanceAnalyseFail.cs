using System;

namespace BusinessLogic.Exceptions
{
    public class AttendanceAnalyseFail : Exception
    {
        public AttendanceAnalyseFail()
        {
        }

        public AttendanceAnalyseFail(string message)
            : base(message)
        { }
    }
}