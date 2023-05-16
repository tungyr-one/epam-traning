using BusinessLogic.Exceptions;
using Domain;
using LecturesApp.BusinessLogic.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    internal class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly ILogger<StudentsService> _logger;

        public StudentsService(IStudentsRepository studentsRepository, ILogger<StudentsService> logger)
        {
            this._studentsRepository = studentsRepository;
            _logger = logger;
        }

        public Student? Get(int id)
        {
            if (_studentsRepository.Get(id) is Student student)
            {
                return student;
            }
            else
            {
                _logger.LogError($"Unable get student with id {id}");
                throw new StudentNotFoundException("No such student in DB.");
            }
        }

        public IReadOnlyCollection<Student> GetAll()
        {
            return _studentsRepository.GetAll().ToArray();
        }

        public int Create(Student student)
        {
            if (_studentsRepository.Create(student) is int studentId && studentId > 0)
            {
                return studentId;
            }
            else
            {
                _logger.LogError($"Unable get student with name {student.Name}");
                throw new StudentException("Unable to create new student entry.");
            }
        }

        public int Edit(Student student)
        {
            if (_studentsRepository.Edit(student) is int studentId && studentId > 0)
            {
                return studentId;
            }
            else
            {
                _logger.LogError($"Unable update student with name {student.Name}");
                throw new StudentNotFoundException("No such student in DB.");
            }
        }

        public int Delete(int id)
        {
            if (_studentsRepository.Delete(id) is int status && status > 0)
            {
                return status;
            }
            else
            {
                _logger.LogError($"Unable delete student with id {id}");
                throw new StudentNotFoundException("No such student in DB.");
            }
        }
    }
}