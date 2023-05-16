using Domain;
using LecturesApp.BusinessLogic.Exceptions;
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
    public class StudentControllerTests
    {
        private Mock<IStudentsService> _studentService;
        private List<Student> _students;
        private StudentController _studentController;

        [SetUp]
        public void Setup()
        {
            _studentService = new Mock<IStudentsService>();
            _students = DataInitializer.GetAllStudents();
            _studentController = new StudentController(_studentService.Object, NullLogger<StudentController>.Instance);
        }

        [Test]
        public void GetStudent_PassCorrectId()
        {
            var testStudent = _students[0];
            _studentService.Setup(s => s.Get(testStudent.Id)).Returns(_students[0]);

            var actual = _studentController.GetStudent(testStudent.Id);
            Assert.That(actual.Value, Is.EqualTo(testStudent));
        }

        [Test]
        public void GetStudent_PassWrongId()
        {
            var testStudentId = 0;
            _studentService.Setup(s => s.Get(testStudentId)).Throws(new StudentNotFoundException());

            Assert.Throws(typeof(StudentNotFoundException), () => _studentController.GetStudent(testStudentId));
        }

        [Test]
        public void GetStudents_ReturnsAllStudents()
        {
            _studentService.Setup(s => s.GetAll()).Returns(_students);

            var actual = _studentController.GetStudents();
            Assert.That(actual.Value, Is.EqualTo(_students));
        }

        [Test]
        public void AddStudent_PassCorrectData()
        {
            //var testStudent = "{\"name\":\"Electronics\",\"date\":\"2021-10-13\",\"professorName\":\"Elon Musk\",\"StudentAttendances\":[]}";
            var testStudent = _students[0];
            _studentService.Setup(s => s.Create(testStudent)).Returns(testStudent.Id);

            var result = _studentController.AddStudent(testStudent);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateStudent_PassCorrectData()
        {
            var testStudent = _students.First();

            _studentService.Setup(s => s.Edit(testStudent)).Returns(testStudent.Id);

            var result = _studentController.UpdateStudent(testStudent.Id, testStudent);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void DeleteStudent_PassStudentId()
        {
            var testStudent = _students.First();

            _studentService.Setup(s => s.Delete(testStudent.Id)).Returns(testStudent.Id);

            var result = _studentController.DeleteStudent(testStudent.Id);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}