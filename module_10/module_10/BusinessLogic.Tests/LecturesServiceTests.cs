using BusinessLogic.Exceptions;
using Domain;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestHelper;

namespace BusinessLogic.Tests
{
    public class LecturesServiceTests
    {
        private Mock<ILecturesRepository> _lecturesRepository = new();
        private ILecturesService _lecturesService;
        private List<Lecture> _lectures;

        [SetUp]
        public void Setup()
        {
            _lectures = DataInitializer.GetAllLectures();
            _lecturesService = new LecturesService(_lecturesRepository.Object, NullLogger<LecturesService>.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _lectures = null;
        }

        [Test]
        public void GetAll_ReturnsList()
        {
            _lecturesRepository.Setup(p => p.GetAll()).Returns(_lectures);
            var actual = _lecturesService.GetAll();
            Assert.IsInstanceOf(typeof(IReadOnlyCollection<Lecture>), actual);
            Assert.That(actual, Is.EqualTo(_lectures));
            Assert.That(_lectures.Count, Is.EqualTo(actual.Count));
        }

        [Test]
        public void GetAll_EmptyRepo_ReturnsEmptyList()
        {
            _lecturesRepository.Setup(p => p.GetAll()).Returns(_lectures);
            _lectures.Clear();
            var lectures = _lecturesService.GetAll();
            Assert.That(lectures.Count, Is.EqualTo(_lectures.Count));
            _lectures = DataInitializer.GetAllLectures();
        }

        [Test]
        public void Get_PassCorrectId()
        {
            var testId = 2;
            _lecturesRepository.Setup(p => p.Get(testId)).Returns(_lectures.Find(lect => lect.Id == testId));
            var physicsLecture = _lecturesService.Get(2);
            Assert.That(physicsLecture.Id, Is.EqualTo(testId));
        }

        [Test]
        public void Get_PassWrongId()
        {
            var testId = 0;
            _lecturesRepository.Setup(p => p.Get(testId)).Returns(() => null);
            Assert.Throws(typeof(LectureNotFoundException), () => _lecturesService.Get(testId));
        }

        [Test]
        public void Create_ReturnsCreatedLectureId()
        {
            var newLecture = new Lecture()
            {
                Name = "Testing",
                Date = new DateTime(2020, 10, 15),
                ProfessorName = "Andrey Bessonov",
                StudentAttendances = new List<StudentAttendance>(),
            };

            _lecturesRepository.Setup(p => p.Create((It.IsAny<Lecture>())))
           .Callback(new Action<Lecture>(newLecture =>
           {
               dynamic maxLectureID = _lectures.Last().Id;
               dynamic nextLectureID = maxLectureID + 1;
               newLecture.Id = nextLectureID;
               _lectures.Add(newLecture);
           })).Returns<Lecture>(lect => lect.Id);

            var createLectureId = _lecturesService.Create(newLecture);

            Assert.That(newLecture, Is.EqualTo(_lectures.Last()));
            Assert.That(createLectureId, Is.EqualTo(_lectures.Last().Id));
        }

        [Test]
        public void Edit_PassCorrectLecture()
        {
            var testLecture = _lectures.First();
            testLecture.Name = "Testing";

            _lecturesRepository.Setup(p => p.Edit(It.IsAny<Lecture>()))
                .Returns(testLecture.Id);

            var updatedProduct = new Lecture()
            { Name = testLecture.Name, Id = testLecture.Id };
            _lecturesService.Edit(updatedProduct);
            Assert.That(testLecture.Id, Is.EqualTo(1));
            Assert.That(testLecture.Name, Is.EqualTo("Testing"));
        }

        [Test]
        public void Edit_PassWrongLecture()
        {
            var testLecture = new Lecture() { Id = 0 };

            _lecturesRepository.Setup(p => p.Edit(testLecture)).Throws<LectureNotFoundException>();
            Assert.Throws(typeof(LectureNotFoundException), () => _lecturesService.Edit(testLecture));
        }

        [Test]
        public void Delete_RemoveLectureById()
        {
            var testLecture = _lectures.Last();

            _lecturesRepository.Setup(p => p.Delete(It.IsAny<int>()))
            .Callback(new Action<int>(Id =>
            {
                var lectureToRemove =
                    _lectures.Find(lect => lect.Id == Id);

                if (lectureToRemove != null)
                    _lectures.Remove(lectureToRemove);
            })).Returns(1);

            int maxID = _lectures.Max(a => a.Id);
            _lecturesService.Delete(testLecture.Id);
            Assert.That(maxID, Is.GreaterThan(_lectures.Max(a => a.Id)));
        }

        [Test]
        public void Delete_RemoveLectureByWrongId()
        {
            var testLecture = new Lecture() { Id = 0 };

            _lecturesRepository.Setup(p => p.Delete(It.IsAny<int>())).Throws<LectureNotFoundException>();

            Assert.Throws(typeof(LectureNotFoundException), () => _lecturesService.Delete(testLecture.Id));
        }
    }
}