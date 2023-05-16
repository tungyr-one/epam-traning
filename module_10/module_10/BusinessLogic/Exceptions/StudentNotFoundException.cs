using BusinessLogic.Exceptions;
using System;

namespace LecturesApp.BusinessLogic.Exceptions
{
    public class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException()
        {
        }

        public StudentNotFoundException(string message) :
            base(message)
        {
        }
    }
}