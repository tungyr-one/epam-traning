namespace Domain
{
    public record StudentAttendance
    {
        public StudentAttendance()
        {
        }

        public int LectureId { get; set; }
        public int StudentId { get; set; }
        public string LectureName { get; set; } = null!;
        public string StudentName { get; set; } = null!;
        public int HomeworkMark { get; set; }
        public bool isPresent { get; set; }
    }
}