using System;

namespace BusinessLogic.Exceptions
{
    public class StudyProgressAnalyseFail : Exception
    {
        public StudyProgressAnalyseFail()
        {
        }

        public StudyProgressAnalyseFail(string message)
            : base(message)
        { }
    }
}