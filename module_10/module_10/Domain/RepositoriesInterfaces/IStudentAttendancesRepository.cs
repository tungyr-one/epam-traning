using System.Collections.Generic;

namespace Domain
{
    public interface IStudentAttendancesRepository
    {
        int Delete(int lectureId, int studentId);

        string Edit(StudentAttendance studentAttendance);

        StudentAttendance? Get(int lectureId, int studentId);

        IEnumerable<StudentAttendance> GetAll();

        string Create(StudentAttendance studentAttendance);

        public IEnumerable<StudentAttendance> GetAllByLectureName(string lectureName);
    }
}