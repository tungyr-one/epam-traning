using BusinessLogic.Exceptions;
using System;

namespace LecturesApp.BusinessLogic.Exceptions
{
    public class StudentAttendanceNotFoundException : NotFoundException
    {
        public StudentAttendanceNotFoundException()
        {
        }

        public StudentAttendanceNotFoundException(string message) :
            base(message)
        {
        }
    }
}