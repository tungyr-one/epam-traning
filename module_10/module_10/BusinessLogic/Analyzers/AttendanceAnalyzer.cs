using Domain;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.BusinessLogic.Notifier
{
    internal class AttendanceAnalyzer : IAttendanceAnalyzer
    {
        public List<StudentAttendance> AnalyseStudentAttendance(Student student)
        {
            List<StudentAttendance> problemAttendances = new();
            var lectureNames = student.StudentAttendances.Select(b => b.LectureName).Distinct().ToList();

            foreach (var lectName in lectureNames)
            {
                var query = student.StudentAttendances.Where(sa => sa.LectureName == lectName && sa.isPresent == false).ToList();

                // TODO: change to > 3
                if (query.Count > 2)
                {
                    problemAttendances.Add(query.First());
                }
            }
            return problemAttendances;
        }

        public List<StudentAttendance> AnalyseLectureAttendance(List<StudentAttendance> attendances)
        {
            List<StudentAttendance> problemAttendances = new();
            var studentNames = attendances.Select(b => b.StudentName).Distinct().ToList();

            foreach (var studName in studentNames)
            {
                var query = attendances.Where(sa => sa.StudentName == studName && sa.isPresent == false).ToList();

                // TODO: change to > 3
                if (query.Count > 2)
                {
                    problemAttendances.Add(query.First());
                }
            }
            return problemAttendances;
        }
    }
}