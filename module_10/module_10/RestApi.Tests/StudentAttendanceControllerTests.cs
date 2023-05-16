using Domain;
using LecturesApp.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RestApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using TestHelper;

namespace RestApi.Tests
{
    public class StudentAttendanceControllerTests
    {
        private Mock<IStudentAttendancesService> _studentAttendanceService;
        private List<StudentAttendance> _studentAttendances;
        private StudentAttendanceController _studentAttendanceController;

        [SetUp]
        public void Setup()
        {
            _studentAttendanceService = new Mock<IStudentAttendancesService>();
            _studentAttendances = DataInitializer.GetAllStudentAttendances();
            _studentAttendanceController = new StudentAttendanceController(_studentAttendanceService.Object);
        }

        [Test]
        public void GetStudentAttendance_PassCorrectId()
        {
            var testAtt = _studentAttendances[0];
            _studentAttendanceService.Setup(s => s.Get(testAtt.LectureId, testAtt.StudentId)).Returns(_studentAttendances[0]);

            var actual = _studentAttendanceController.GetStudentAttendance(testAtt.LectureId, testAtt.StudentId);
            Assert.That(actual.Value, Is.EqualTo(testAtt));
        }

        [Test]
        public void GetStudentAttendance_PassWrongId()
        {
            var testAtt = _studentAttendances[0];
            _studentAttendanceService.Setup(s => s.Get(testAtt.LectureId, testAtt.StudentId)).Throws(new StudentAttendanceNotFoundException());

            Assert.Throws(typeof(StudentAttendanceNotFoundException), () => _studentAttendanceController.GetStudentAttendance(testAtt.LectureId, testAtt.StudentId));
        }

        [Test]
        public void GetStudentAttendances_ReturnsAllStudentAttendances()
        {
            _studentAttendanceService.Setup(s => s.GetAll()).Returns(_studentAttendances);

            var actual = _studentAttendanceController.GetStudentAttendances();
            Assert.That(actual.Value, Is.EqualTo(_studentAttendances));
        }

        [Test]
        public void AddStudentAttendance_PassCorrectData()
        {
            var testAtt = _studentAttendances[0];
            _studentAttendanceService.Setup(s => s.Create(testAtt)).Returns(new string($"{testAtt.LectureId}, {testAtt.StudentId}"));

            var result = _studentAttendanceController.AddStudentAttendance(testAtt);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateStudentAttendance_PassCorrectData()
        {
            var testAtt = _studentAttendances.First();

            _studentAttendanceService.Setup(s => s.Edit(testAtt)).Returns(new string($"{testAtt.LectureId}, {testAtt.StudentId}"));

            var result = _studentAttendanceController.UpdateStudentAttendance(testAtt.LectureId, testAtt.StudentId, testAtt);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void DeleteStudentAttendance_PassStudentAttendanceId()
        {
            var testAtt = _studentAttendances.First();

            _studentAttendanceService.Setup(s => s.Delete(testAtt.LectureId, testAtt.StudentId)).Returns(1);

            var result = _studentAttendanceController.DeleteStudentAttendance(testAtt.LectureId, testAtt.StudentId);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}