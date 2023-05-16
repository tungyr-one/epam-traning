namespace DataAccess.Models
{
    internal record StudentAttendanceDb
    {
        public int LectureId { get; set; }
        public LectureDb Lecture { get; set; }

        public int StudentId { get; set; }
        public StudentDb Student { get; set; }

        public int HomeworkMark { get; set; }
        public bool IsPresent { get; set; }
    }
}