using System;
using System.Collections.Generic;

namespace Domain
{
    public record Lecture
    {
        public Lecture()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }

        public string ProfessorName { get; set; } = null!;
        public List<StudentAttendance> StudentAttendances { get; set; } = null!;
    }
}