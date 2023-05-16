using AutoMapper;
using AutoMapper.Features;
using BusinessLogic.Exceptions;
using DataAccess.Models;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class StudentAttendanceRepository : IStudentAttendancesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentAttendanceRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public IEnumerable<StudentAttendance> GetAll()
        {
            var studentAttendancesDb = _context.StudentAttendances
                .Include(sa => sa.Lecture)
                .Include(sa => sa.Student)
                .AsNoTracking()
                .ToList();
            //var studentAttendancesDb = _context.StudentAttendances
            //.Include(sa => sa.Lecture)
            //.Include(sa => sa.Student)
            //.ToList();
            return _mapper.Map<IReadOnlyCollection<StudentAttendance>>(studentAttendancesDb);
        }

        public IEnumerable<StudentAttendance> GetAllByLectureName(string lectureName)
        {
            var studentAttendancesDb = _context.StudentAttendances.Where(sa => sa.Lecture.Name == lectureName)
                .Include(sa => sa.Lecture)
                .Include(sa => sa.Student)
                .ToList();
            return _mapper.Map<IReadOnlyCollection<StudentAttendance>>(studentAttendancesDb);
        }

        public StudentAttendance Get(int lectureId, int studentId)
        {
            var studentAttendanceDb = _context.StudentAttendances
                .Where(sa => sa.LectureId == lectureId && sa.StudentId == studentId)
                .Include(sa => sa.Lecture)
                .Include(sa => sa.Student)
                .FirstOrDefault();
            return _mapper.Map<StudentAttendance?>(studentAttendanceDb);
        }

        public string Create(StudentAttendance studentAttendance)
{
            if (_context.Lectures.Find(studentAttendance.LectureId) is null && _context.Students.Find(studentAttendance.StudentId) is null)
            {
                throw new StudentAttendanceException("No such lecture or student. Insert them first.");
            }

            if (_context.StudentAttendances.Find(studentAttendance.LectureId, studentAttendance.StudentId) is not null)
            {
                throw new StudentAttendanceException("Such student attandance already exist.");
            }

            var newLectureAtt = new StudentAttendanceDb()
            {
                Lecture = _context.Lectures.Find(studentAttendance.LectureId),
                Student = _context.Students.Find(studentAttendance.StudentId),
                IsPresent = studentAttendance.isPresent,
                HomeworkMark = studentAttendance.HomeworkMark,
            };

            var result = _context.StudentAttendances.Add(newLectureAtt);
            _context.SaveChanges();
            return new string($"{result.Entity.LectureId}, {result.Entity.StudentId}");
        }

        public string Edit(StudentAttendance studentAttendance)
        {
            if (_context.StudentAttendances.Find(studentAttendance.LectureId, studentAttendance.StudentId) is StudentAttendanceDb studentAttendanceInDb)
            {
                studentAttendanceInDb.LectureId = studentAttendance.LectureId;
                studentAttendanceInDb.StudentId = studentAttendance.StudentId;
                studentAttendanceInDb.IsPresent = studentAttendance.isPresent;
                studentAttendanceInDb.HomeworkMark = studentAttendance.HomeworkMark;
                _context.Entry(studentAttendanceInDb).State = EntityState.Modified;
                _context.SaveChanges();
                return new string($"{studentAttendanceInDb.LectureId}, {studentAttendanceInDb.StudentId}");
            }
            else
            {
                return null;
            }
        }

        public int Delete(int lectureId, int studentId)
        {
            if (_context.StudentAttendances.Find(lectureId, studentId) is StudentAttendanceDb studentAttendanceToDelete)
            {
                _context.Entry(studentAttendanceToDelete).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}