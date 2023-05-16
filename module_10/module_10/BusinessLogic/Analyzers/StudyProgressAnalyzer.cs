using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.BusinessLogic.Notifier
{
    internal class StudyProgressAnalyzer : IStudyProgressAnalyzer
    {
        public List<Tuple<StudentAttendance, double>> AnalyseStudentStudy(List<StudentAttendance> attendances)
        {
            List<Tuple<StudentAttendance, double>> problemAttendances = new();
            var lectureNames = attendances.Select(b => b.LectureName).Distinct().ToList();

            foreach (var lectName in lectureNames)
            {
                var query = attendances.Where(sa => sa.LectureName == lectName && sa.HomeworkMark > 0).ToList();

                if (query.Count >= 3)
                {
                    var averageMark = query.Select(sa => sa.HomeworkMark).Average();

                    if (averageMark < 4)
                    {
                        problemAttendances.Add(new Tuple<StudentAttendance, double>(query.First(), averageMark));
                    }
                }
                else
                    continue;
            }
            return problemAttendances;
        }

        public List<Tuple<StudentAttendance, double>> AnalyseLectureStudy(List<StudentAttendance> attendances)
        {
            List<Tuple<StudentAttendance, double>> problemAttendances = new();
            var studentNames = attendances.Select(b => b.StudentName).Distinct().ToList();

            foreach (var studName in studentNames)
            {
                var query = attendances.Where(sa => sa.StudentName == studName && sa.HomeworkMark > 0).ToList();

                if (query.Count >= 3)
                {
                    var averageMark = query.Select(sa => sa.HomeworkMark).Average();

                    if (averageMark < 4)
                    {
                        problemAttendances.Add(new Tuple<StudentAttendance, double>(query.First(), averageMark));
                    }
                }
                else
                    continue;
            }
            return problemAttendances;
        }
    }
}