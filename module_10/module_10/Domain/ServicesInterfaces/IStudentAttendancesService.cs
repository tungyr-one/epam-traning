using System.Collections.Generic;

namespace Domain
{
    public interface IStudentAttendancesService
    {
        StudentAttendance? Get(int lectureId, int studentId);

        IReadOnlyCollection<StudentAttendance> GetAll();

        string Create(StudentAttendance studentAttendance);

        string Edit(StudentAttendance studentAttendance);

        int Delete(int lectureId, int studentId);
    }
}