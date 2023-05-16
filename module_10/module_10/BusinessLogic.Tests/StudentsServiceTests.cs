using Domain;
using LecturesApp.BusinessLogic.Exceptions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestHelper;

namespace BusinessLogic.Tests
{
    public class StudentsServiceTests
    {
        private Mock<IStudentsRepository> _studentsRepository = new();
        private IStudentsService _studentsService;
        private List<Student> _students;

        [SetUp]
        public void Setup()
        {
            _students = DataInitializer.GetAllStudents();
            _studentsService = new StudentsService(_studentsRepository.Object, NullLogger<StudentsService>.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _students = null;
        }

        [Test]
        public void GetAll_ReturnsList()
        {
            _studentsRepository.Setup(p => p.GetAll()).Returns(_students);
            var actual = _studentsService.GetAll();

            Assert.That(actual, Is.EqualTo(_students));
        }

        [Test]
        public void GetAll_EmptyRepo_ReturnsEmptyList()
        {
            _studentsRepository.Setup(p => p.GetAll()).Returns(_students);
            _students.Clear();
            var students = _studentsService.GetAll();
            Assert.That(students.Count, Is.EqualTo(_students.Count));
            _students = DataInitializer.GetAllStudents();
        }

        [Test]
        public void Get_PassCorrectId()
        {
            var testId = 2;
            _studentsRepository.Setup(p => p.Get(testId)).Returns(_students.Find(prof => prof.Id == testId));
            var physicsStudent = _studentsService.Get(2);
            if (physicsStudent != null)
                Assert.That(physicsStudent.Id, Is.EqualTo(testId));
        }

        [Test]
        public void Get_PassWrongId()
        {
            var testId = 0;
            _studentsRepository.Setup(p => p.Get(testId)).Returns(() => null);
            Assert.Throws(typeof(StudentNotFoundException), () => _studentsService.Get(testId));
        }

        [Test]
        public void Create_ReturnsCreatedStudentId()
        {
            var newStudent = new Student()
            {
                Name = "Andrey Bessonov",
                Email = "a.b@university.com",
            };

            _studentsRepository.Setup(p => p.Create((It.IsAny<Student>())))
           .Callback(new Action<Student>(newStudent =>
           {
               dynamic maxStudentID = _students.Last().Id;
               dynamic nextStudentID = maxStudentID + 1;
               newStudent.Id = nextStudentID;
               _students.Add(newStudent);
           })).Returns<Student>(prof => prof.Id);

            var createStudentId = _studentsService.Create(newStudent);

            Assert.That(newStudent, Is.EqualTo(_students.Last()));
            Assert.That(createStudentId, Is.EqualTo(_students.Last().Id));
        }

        [Test]
        public void Edit_PassCorrectStudent()
        {
            var testStudent = _students.First();
            _studentsRepository.Setup(p => p.Edit(It.IsAny<Student>()))
               .Callback(new Action<Student>(prof =>
               {
                   dynamic oldStudent = _students.Find(a => a.Id == prof.Id);
                   oldStudent = prof;
               })).Returns(testStudent.Id);

            var updatedStudent = new Student()
            {
                Id = testStudent.Id,
                Name = "Andrey Bessonov",
                Email = "a.b@university.com",
            };
            _studentsService.Edit(updatedStudent);
            Assert.That(testStudent.Id, Is.EqualTo(1));
            Assert.That(testStudent.Name, Is.EqualTo(_students.First().Name));
        }

        [Test]
        public void Edit_PassWrongStudent()
        {
            var testStudent = new Student() { Id = 0 };

            _studentsRepository.Setup(p => p.Edit(testStudent)).Throws<StudentNotFoundException>();
            Assert.Throws(typeof(StudentNotFoundException), () => _studentsService.Edit(testStudent));
        }

        [Test]
        public void Delete_RemoveStudentById()
        {
            var testStudent = _students.Last();

            _studentsRepository.Setup(p => p.Delete(It.IsAny<int>()))
            .Callback(new Action<int>(Id =>
            {
                var studentToRemove =
                    _students.Find(prof => prof.Id == Id);

                if (studentToRemove != null)
                    _students.Remove(studentToRemove);
            })).Returns(1);

            int maxID = _students.Max(a => a.Id);
            _studentsService.Delete(testStudent.Id);
            Assert.That(maxID, Is.GreaterThan(_students.Max(a => a.Id)));
        }

        [Test]
        public void Delete_RemoveLectureByWrongId()
        {
            var testLecture = new Student() { Id = 0 };

            _studentsRepository.Setup(p => p.Delete(It.IsAny<int>())).Throws<StudentNotFoundException>();

            Assert.Throws(typeof(StudentNotFoundException), () => _studentsService.Delete(testLecture.Id));
        }
    }
}