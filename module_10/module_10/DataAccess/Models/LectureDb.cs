using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    internal record LectureDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int ProfessorId { get; set; }
        public ProfessorDb Professor { get; set; }

        public ICollection<StudentAttendanceDb> StudentAttendances { get; set; }
    }
}