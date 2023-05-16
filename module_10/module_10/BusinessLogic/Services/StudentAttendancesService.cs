using BusinessLogic.Exceptions;
using Domain;
using LecturesApp.BusinessLogic.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    internal class StudentAttendancesService : IStudentAttendancesService
    {
        private readonly IStudentAttendancesRepository _studentAttendancesRepository;
        private readonly ILogger<StudentAttendancesService> _logger;

        public StudentAttendancesService(IStudentAttendancesRepository studentAttendancesRepository, ILogger<StudentAttendancesService> logger)
        {
            _studentAttendancesRepository = studentAttendancesRepository;
            _logger = logger;
        }

        public StudentAttendance? Get(int lectureId, int studentId)
        {
            if (_studentAttendancesRepository.Get(lectureId, studentId) is StudentAttendance studentAttendance)
            {
                return studentAttendance;
            }
            else
            {
                _logger.LogError($"Unable get students attendance with id {lectureId} {studentId}");
                throw new StudentAttendanceNotFoundException("No such students attendance in DB.");
            }
        }

        public IReadOnlyCollection<StudentAttendance> GetAll()
        {
            return _studentAttendancesRepository.GetAll().ToArray();
        }

        public string Create(StudentAttendance studentAttendance)
        {     
            return _studentAttendancesRepository.Create(studentAttendance);
        }

        public string Edit(StudentAttendance studentAttendance)
        {
            if (_studentAttendancesRepository.Edit(studentAttendance) is string saId && saId != null)
            {
                return saId;
            }
            else
            {
                _logger.LogError($"Unable update students attendance for student {studentAttendance.StudentName}");
                throw new StudentAttendanceNotFoundException("No such students attendance in DB.");
            }
        }

        public int Delete(int lectureId, int studentId)
        {
            if (_studentAttendancesRepository.Delete(lectureId, studentId) is int status && status > 0)
            {
                return status;
            }
            else
            {
                _logger.LogError($"Unable delete students attendance with id {lectureId} {studentId}");
                throw new StudentAttendanceNotFoundException("No such students attendance in DB.");
            }
        }
    }
}