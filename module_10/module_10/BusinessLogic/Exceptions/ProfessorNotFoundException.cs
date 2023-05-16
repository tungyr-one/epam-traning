using BusinessLogic.Exceptions;
using System;

namespace LecturesApp.BusinessLogic.Exceptions
{
    public class ProfessorNotFoundException : NotFoundException
    {
        public ProfessorNotFoundException()
        {
        }

        public ProfessorNotFoundException(string message) :
            base(message)
        {
        }
    }
}