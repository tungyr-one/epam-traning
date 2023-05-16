using AutoMapper;
using DataAccess.Models;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    internal class LecturesRepository : ILecturesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LecturesRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public IEnumerable<Lecture> GetAll()
        {
            var lecturesDb = _context.Lectures
                .Include(l => l.Professor)
                .ToList();
            return _mapper.Map<IReadOnlyCollection<Lecture>>(lecturesDb);
        }

        public Lecture Get(int id)
        {
            var lectureDb = _context.Lectures.Where(lect => lect.Id == id)
                .Include(lect => lect.Professor)
                .AsNoTracking()
                .FirstOrDefault();
            return _mapper.Map<Lecture>(lectureDb);
        }

        public Lecture GetByName(string name)
        {
            var lectureDb = _context.Lectures.Where(lect => lect.Name == name)
                .Include(lect => lect.Professor)
                .Include(lect => lect.StudentAttendances)
                .ThenInclude(x => x.Student)
                .AsNoTracking()
                .FirstOrDefault();
            return _mapper.Map<Lecture>(lectureDb);
        }

        public IEnumerable<Lecture> GetAllIncludedEntities(string name)
        {
            var lecturesDb = _context.Lectures.Where(lect => lect.Name == name)
                .Include(lect => lect.Professor)
                .Include(lect => lect.StudentAttendances)
                .ThenInclude(x => x.Student)
                .AsNoTracking();
            return _mapper.Map<IReadOnlyCollection<Lecture>>(lecturesDb);
        }

        public int Create(Lecture lecture)
        {
            var lectureDb = _mapper.Map<LectureDb>(lecture);

            lectureDb.Professor = _context.Professors
                .Where(prof => prof.Name == lecture.ProfessorName)
                .FirstOrDefault();

            lectureDb.StudentAttendances = new List<StudentAttendanceDb>();

            var result = _context.Lectures.Add(lectureDb);

            CreateLecturesDefaultStudentAttendances(result.Entity);

            _context.SaveChanges();

            return result.Entity.Id;
        }

        //TODO: remove it
        public Professor GetLectureProfessor(string lectureName)
        {
            var professorDb = _context.Lectures.Where(lect => lect.Name == lectureName).Include(lect => lect.Professor).FirstOrDefault().Professor;
            return _mapper.Map<Professor>(professorDb);
        }

        public int Edit(Lecture lecture)
        {
            if (_context.Lectures.Find(lecture.Id) is LectureDb lectureInDb)
            {
                lectureInDb.Name = lecture.Name;
                lectureInDb.Date = lecture.Date;
                lectureInDb.Professor = _context.Professors.Where(prof => prof.Name == lecture.ProfessorName).FirstOrDefault();
                StudentAttendanceRepository updater = new(_context, _mapper);
                foreach (StudentAttendance sa in lecture.StudentAttendances)
                {
                    updater.Edit(sa);
                }
                _context.Entry(lectureInDb).State = EntityState.Modified;
                _context.SaveChanges();
                return lectureInDb.Id; ;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            if (_context.Lectures.Find(id) is LectureDb lectureToDelete)
            {
                _context.Entry(lectureToDelete).State = EntityState.Deleted;
                _context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        internal void CreateLecturesDefaultStudentAttendances(LectureDb lecture)
        {
            foreach (StudentDb st in _context.Students)
            {
                StudentAttendanceDb sa = new();
                sa.Lecture = lecture;
                sa.Student = st;
                sa.IsPresent = false;
                sa.HomeworkMark = 0;
                _context.StudentAttendances.Add(sa);
                lecture.StudentAttendances.Add(sa);
            }
        }
    }
}