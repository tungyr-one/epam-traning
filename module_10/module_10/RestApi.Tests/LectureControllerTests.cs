using BusinessLogic.Exceptions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using RestApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using TestHelper;

namespace RestApi.Tests
{
    public class LectureControllerTests
    {
        private Mock<ILecturesService> _lectureService;
        private List<Lecture> _lectures;
        private LectureController _lectureController;

        [SetUp]
        public void Setup()
        {
            _lectureService = new Mock<ILecturesService>();
            _lectures = DataInitializer.GetAllLectures();
            _lectureController = new LectureController(_lectureService.Object, NullLogger<LectureController>.Instance);
        }

        [Test]
        public void GetLecture_PassCorrectId()
        {
            var testLecture = _lectures[0];
            _lectureService.Setup(s => s.Get(testLecture.Id)).Returns(_lectures[0]);

            var actual = _lectureController.GetLecture(testLecture.Id);
            Assert.That(actual.Value, Is.EqualTo(testLecture));
        }

        [Test]
        public void GetLecture_PassWrongId()
        {
            var testLectureId = 0;
            _lectureService.Setup(s => s.Get(testLectureId)).Throws(new LectureNotFoundException());

            Assert.Throws(typeof(LectureNotFoundException), () => _lectureController.GetLecture(testLectureId));
        }

        [Test]
        public void GetLectures_ReturnsAllLectures()
        {
            _lectureService.Setup(s => s.GetAll()).Returns(_lectures);

            var actual = _lectureController.GetLectures();
            Assert.That(actual.Value, Is.EqualTo(_lectures));
        }

        [Test]
        public void AddLecture_PassCorrectData()
        {
            var testLecture = _lectures[0];
            _lectureService.Setup(s => s.Create(testLecture)).Returns(testLecture.Id);

            var result = _lectureController.AddLecture(testLecture);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateLecture_PassCorrectData()
        {
            var testLecture = _lectures.First();

            _lectureService.Setup(s => s.Edit(testLecture)).Returns(testLecture.Id);

            var result = _lectureController.UpdateLecture(testLecture.Id, testLecture);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void DeleteLecture_PassLectureId()
        {
            var testLecture = _lectures.First();

            _lectureService.Setup(s => s.Delete(testLecture.Id)).Returns(testLecture.Id);

            var result = _lectureController.DeleteLecture(testLecture.Id);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}