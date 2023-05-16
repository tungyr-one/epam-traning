using System;

namespace StudentsAssesments
{
    public class StudentAssessment
    {
        public string Name { get; set; }
        public string Test{ get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }

        public StudentAssessment(string name, string test, string date, int mark)
        {
            Name = name;
            Test = test;
            Date = Convert.ToDateTime(date);            
            Mark = mark;
        }
    }
}
