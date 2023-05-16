using System.Collections.Generic;

namespace Domain
{
    public record Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int Age { get; set; }
        public List<StudentAttendance> StudentAttendances { get; set; } = null!;
    }
}