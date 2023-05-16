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
    public class StudentAttendancesServiceTests
    {
        private Mock<IStudentAttendancesRepository> _studentAttendancesRepository = new();
        private IStudentAttendancesService _studentAttendancesService;
        private List<StudentAttendance> _studentAttendances;

        [SetUp]
        public void Setup()
        {
            _studentAttendances = DataInitializer.GetAllStudentAttendances();
            _studentAttendancesService = new StudentAttendancesService(_studentAttendancesRepository.Object, NullLogger<StudentAttendancesService>.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _studentAttendances = null;
        }

        [Test]
        public void GetAll_ReturnsList()
        {
            _studentAttendancesRepository.Setup(p => p.GetAll()).Returns(_studentAttendances);
            var actual = _studentAttendancesService.GetAll();

            Assert.That(actual, Is.EqualTo(_studentAttendances));
        }

        [Test]
        public void GetAll_EmptyRepo_ReturnsEmptyList()
        {
            _studentAttendancesRepository.Setup(p => p.GetAll()).Returns(_studentAttendances);
            _studentAttendances.Clear();
            var studentAttendances = _studentAttendancesService.GetAll();
            Assert.That(studentAttendances.Count, Is.EqualTo(_studentAttendances.Count));
            _studentAttendances = DataInitializer.GetAllStudentAttendances();
        }

        [Test]
        public void Get_PassCorrectId()
        {
            var testLectId = 1;
            var testStudId = 2;
            var testStudentAttendance = _studentAttendances.Find(p => p.LectureId == testLectId && p.StudentId == testStudId);
            _studentAttendancesRepository.Setup(p => p.Get(testLectId, testStudId))
                .Returns(testStudentAttendance);
            var physicsStudentAttendance = _studentAttendancesService.Get(testLectId, testStudId);
            if (physicsStudentAttendance != null)
                Assert.That(physicsStudentAttendance, Is.EqualTo(testStudentAttendance));
        }

        [Test]
        public void Get_PassWrongId()
        {
            var testLectId = 1;
            var testStudId = 0;
            _studentAttendancesRepository.Setup(p => p.Get(testLectId, testStudId)).Returns(() => null);
            Assert.Throws(typeof(StudentAttendanceNotFoundException), () => _studentAttendancesService.Get(testLectId, testStudId));
        }

        [Test]
        public void Create_ReturnsCreatedStudentAttendanceId()
        {
            var newStudentAttendance = new StudentAttendance()
            {
                LectureId = 5,
                LectureName = "Testing",
                StudentName = "Sergei Sidorov",
                HomeworkMark = 0,
                isPresent = false,
            };

            _studentAttendancesRepository.Setup(p => p.Create((It.IsAny<StudentAttendance>())))
           .Callback(new Action<StudentAttendance>(newStudentAttendance =>
           {
               dynamic maxStudentAttendanceID = _studentAttendances.Last().StudentId;
               dynamic nextStudentAttendanceID = maxStudentAttendanceID + 1;
               newStudentAttendance.StudentId = nextStudentAttendanceID;
               _studentAttendances.Add(newStudentAttendance);
           })).Returns<StudentAttendance>(x => x.LectureId.ToString() + ", " + x.StudentId.ToString());

            var createStudentAttendanceId = _studentAttendancesService.Create(newStudentAttendance);

            Assert.That(newStudentAttendance, Is.EqualTo(_studentAttendances.Last()));
        }

        [Test]
        public void Edit_PassCorrectStudentAttendance()
        {
            var testSA = _studentAttendances.First();
            _studentAttendancesRepository.Setup(p => p.Edit(It.IsAny<StudentAttendance>()))
               .Callback(new Action<StudentAttendance>(sa =>
               {
                   dynamic oldStudentAttendance = _studentAttendances.Find(p => p.LectureId == sa.LectureId && p.StudentId == sa.StudentId);
                   oldStudentAttendance = sa;
               })).Returns(new string($"{testSA.LectureId} {testSA.StudentId}"));

            var updatedStudentAttendance = new StudentAttendance()
            {
                LectureId = testSA.LectureId,
                StudentId = testSA.StudentId,
                LectureName = "Shmysics",
                StudentName = "Igor Nikolaev",
            };
            _studentAttendancesService.Edit(updatedStudentAttendance);
            Assert.That(testSA.LectureName, Is.EqualTo(_studentAttendances.First().LectureName));
            Assert.That(testSA.StudentName, Is.EqualTo(_studentAttendances.First().StudentName));
        }

        [Test]
        public void Edit_PassWrongStudentAttendance()
        {
            var testStudentAttendance = new StudentAttendance() { LectureId = 0, StudentId = 0 };

            _studentAttendancesRepository.Setup(p => p.Edit(testStudentAttendance)).Throws<StudentAttendanceNotFoundException>();
            Assert.Throws(typeof(StudentAttendanceNotFoundException), () => _studentAttendancesService.Edit(testStudentAttendance));
        }

        [Test]
        public void Delete_RemoveStudentAttendanceById()
        {
            var testSA = _studentAttendances.Last();

            _studentAttendancesRepository.Setup(p => p.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Callback(new Action<int, int>((lectId, studId) =>
                {
                    dynamic studentAttendanceToRemove =
                        _studentAttendances.Find(s => s.LectureId == lectId && s.StudentId == studId);

                    if (studentAttendanceToRemove != null)
                        _studentAttendances.Remove(studentAttendanceToRemove);
                })).Returns(1);

            int countBefore = _studentAttendances.Count;
            _studentAttendancesService.Delete(testSA.LectureId, testSA.StudentId);
            Assert.That(countBefore, Is.GreaterThan(_studentAttendances.Count));
        }

        [Test]
        public void Delete_RemoveStudentAttendanceByWrongId()
        {
            var testSA = new StudentAttendance() { LectureId = 0, StudentId = 0 };

            _studentAttendancesRepository.Setup(p => p.Delete(It.IsAny<int>(), It.IsAny<int>()))
                .Throws<StudentAttendanceNotFoundException>();

            Assert.Throws(typeof(StudentAttendanceNotFoundException), () => _studentAttendancesService.Delete(testSA.LectureId, testSA.StudentId));
        }
    }
}