using AutoMapper;
using DataAccess.Models;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class StudentsRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentsRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public IEnumerable<Student> GetAll()
        {
            var studentsDb = _context.Students
                .ToList();
            return _mapper.Map<IReadOnlyCollection<Student>>(studentsDb);
        }

        public Student Get(int id)
        {
            var studentDb = _context.Students
                .Where(stud => stud.Id == id)
                .FirstOrDefault();
            return _mapper.Map<Student>(studentDb);
        }

        public Student GetByName(string name)
        {
            var studentDb = _context.Students
                .Where(stud => stud.Name == name)
                .Include(st => st.StudentAttendances)
                .ThenInclude(sa => sa.Lecture)
                .FirstOrDefault();
            return _mapper.Map<Student>(studentDb);
        }

        public int Create(Student student)
        {
            var studentDb = _mapper.Map<StudentDb>(student);
            studentDb.StudentAttendances = new List<StudentAttendanceDb>();
            var result = _context.Students.Add(studentDb);
            _context.SaveChanges();
            return result.Entity.Id;
        }

        public int Edit(Student student)
        {
            if (_context.Students.Find(student.Id) is StudentDb studentInDb)
            {
                studentInDb.Name = student.Name;
                studentInDb.Email = student.Email;
                studentInDb.Phone = student.Phone;
                studentInDb.Age = student.Age;
                _context.Entry(studentInDb).State = EntityState.Modified;
                _context.SaveChanges();
                return studentInDb.Id;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            if (_context.Students.Find(id) is StudentDb studentToDelete)
            {
                _context.Entry(studentToDelete).State = EntityState.Deleted;
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