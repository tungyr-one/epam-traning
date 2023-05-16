using System;

namespace BusinessLogic.Exceptions
{
    public class ReportGenerationFailException : Exception
    {
        public ReportGenerationFailException()
        {
        }

        public ReportGenerationFailException(string message)
            : base(message)
        { }
    }
}