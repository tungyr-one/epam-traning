using Domain;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IAttendanceAnalyzer
    {
        public List<StudentAttendance> AnalyseStudentAttendance(Student student);

        public List<StudentAttendance> AnalyseLectureAttendance(List<StudentAttendance> attendances);
    }
}