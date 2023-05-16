using Domain;
using Domain.Domain.ServicesInterfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using RestApi.Controllers;
using System;
using System.Collections.Generic;
using TestHelper;

namespace RestApi.Tests
{
    public class ReportControllerTests
    {
        private Mock<IReportsService> _reportService;
        private List<Lecture> _lectures;
        private List<Student> _students;
        private List<StudentAttendance> _studentAttendances;
        private ReportController _reportController;

        [SetUp]
        public void Setup()
        {
            _reportService = new Mock<IReportsService>();
            _lectures = DataInitializer.GetAllLectures();
            _students = DataInitializer.GetAllStudents();
            _studentAttendances = DataInitializer.GetAllStudentAttendances();
            _reportController = new ReportController(_reportService.Object, NullLogger<ReportController>.Instance);
        }

        [Test]
        public void GetLectureReport_PassCorrectLectureName()
        {
            var testLecture = _lectures[0];
            _reportService.Setup(s => s.GenerateLectureAttendanceReport(testLecture.Name)).Returns("test string");
            _reportService.Setup(s => s.LectureAttendanceCheck(testLecture.Name)).Returns(_studentAttendances);
            _reportService.Setup(s => s.LectureStudyProgressCheck(testLecture.Name)).Returns(new List<Tuple<StudentAttendance, double>>() { new Tuple<StudentAttendance, double>(_studentAttendances[0], 3.5) });

            var actual = _reportController.GetLectureReport(testLecture.Name);
            Assert.That(actual.Value, Is.EqualTo("test string"));
        }

        [Test]
        public void GetStudentReport_PassCorrectStudenntName()
        {
            var testStudent = _students[0];
            _reportService.Setup(s => s.GenerateStudentAttendanceReport(testStudent.Name)).Returns("test string");
            _reportService.Setup(s => s.StudentAttendanceCheck(testStudent.Name)).Returns(_studentAttendances);
            _reportService.Setup(s => s.StudentStudyProgressCheck(testStudent.Name)).Returns(new List<Tuple<StudentAttendance, double>>() { new Tuple<StudentAttendance, double>(_studentAttendances[0], 3.5) });

            var actual = _reportController.GetStudentReport(testStudent.Name);
            Assert.That(actual.Value, Is.EqualTo("test string"));
        }
    }
}