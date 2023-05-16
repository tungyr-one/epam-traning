using System;
using System.Collections.Generic;

namespace Domain.Domain.ServicesInterfaces
{
    public interface IReportsService
    {
        string GenerateLectureAttendanceReport(string name);

        List<StudentAttendance> LectureAttendanceCheck(string name);

        public List<Tuple<StudentAttendance, double>> LectureStudyProgressCheck(string name);

        string GenerateStudentAttendanceReport(string name);

        public List<StudentAttendance> StudentAttendanceCheck(string name);

        public List<Tuple<StudentAttendance, double>> StudentStudyProgressCheck(string name);

        public List<Student> GenerateStudentsMailList(List<StudentAttendance> attendances);

        public List<Professor> GenerateProfessorsMailList(List<StudentAttendance> attendances);
    }
}