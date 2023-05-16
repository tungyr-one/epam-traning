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
    public class ProfessorControllerTests
    {
        private Mock<IProfessorsService> _professorService;
        private List<Professor> _professors;
        private ProfessorController _professorController;

        [SetUp]
        public void Setup()
        {
            _professorService = new Mock<IProfessorsService>();
            _professors = DataInitializer.GetAllProfessors();
            _professorController = new ProfessorController(_professorService.Object);
        }

        [Test]
        public void GetProfessor_PassCorrectId()
        {
            var testProfessor = _professors[0];
            _professorService.Setup(s => s.Get(testProfessor.Id)).Returns(_professors[0]);

            var actual = _professorController.GetProfessor(testProfessor.Id);
            Assert.That(actual.Value, Is.EqualTo(testProfessor));
        }

        [Test]
        public void GetProfessor_PassWrongId()
        {
            var testProfessorId = 0;
            _professorService.Setup(s => s.Get(testProfessorId)).Throws(new ProfessorNotFoundException());

            Assert.Throws(typeof(ProfessorNotFoundException), () => _professorController.GetProfessor(testProfessorId));
        }

        [Test]
        public void GetProfessors_ReturnsAllProfessors()
        {
            _professorService.Setup(s => s.GetAll()).Returns(_professors);

            var actual = _professorController.GetProfessors();
            Assert.That(actual.Value, Is.EqualTo(_professors));
        }

        [Test]
        public void AddProfessor_PassCorrectData()
        {
            var testProfessor = _professors[0];
            _professorService.Setup(s => s.Create(testProfessor)).Returns(testProfessor.Id);

            var result = _professorController.AddProfessor(testProfessor);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void UpdateProfessor_PassCorrectData()
        {
            var testProfessor = _professors.First();

            _professorService.Setup(s => s.Edit(testProfessor)).Returns(testProfessor.Id);

            var result = _professorController.UpdateProfessor(testProfessor.Id, testProfessor);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void DeleteProfessor_PassProfessorId()
        {
            var testProfessor = _professors.First();

            _professorService.Setup(s => s.Delete(testProfessor.Id)).Returns(testProfessor.Id);

            var result = _professorController.DeleteProfessor(testProfessor.Id);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}