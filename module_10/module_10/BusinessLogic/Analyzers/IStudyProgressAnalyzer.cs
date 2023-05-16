using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IStudyProgressAnalyzer
    {
        public List<Tuple<StudentAttendance, double>> AnalyseStudentStudy(List<StudentAttendance> attendances);

        public List<Tuple<StudentAttendance, double>> AnalyseLectureStudy(List<StudentAttendance> attendances);
    }
}