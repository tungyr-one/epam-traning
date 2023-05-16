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
    public class ProfessorsServiceTests
    {
        private Mock<IProfessorsRepository> _professorsRepository = new();
        private IProfessorsService _professorsService;
        private List<Professor> _professors;

        [SetUp]
        public void Setup()
        {
            _professors = DataInitializer.GetAllProfessors();
            _professorsService = new ProfessorsService(_professorsRepository.Object, NullLogger<ProfessorsService>.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _professors = null;
        }

        [Test]
        public void GetAll_ReturnsList()
        {
            _professorsRepository.Setup(p => p.GetAll()).Returns(_professors);
            var actual = _professorsService.GetAll();

            Assert.That(actual, Is.EqualTo(_professors));
        }

        [Test]
        public void GetAll_EmptyRepo_ReturnsEmptyList()
        {
            _professorsRepository.Setup(p => p.GetAll()).Returns(_professors);
            _professors.Clear();
            var professors = _professorsService.GetAll();
            Assert.That(professors.Count, Is.EqualTo(_professors.Count));
            _professors = DataInitializer.GetAllProfessors();
        }

        [Test]
        public void Get_PassCorrectId()
        {
            var testId = 2;
            _professorsRepository.Setup(p => p.Get(testId)).Returns(_professors.Find(prof => prof.Id == testId));
            var physicsProfessor = _professorsService.Get(2);
            if (physicsProfessor != null)
                Assert.That(physicsProfessor.Id, Is.EqualTo(testId));
        }

        [Test]
        public void Get_PassWrongId()
        {
            var testId = 0;
            _professorsRepository.Setup(p => p.Get(testId)).Returns(() => null);
            Assert.Throws(typeof(ProfessorNotFoundException), () => _professorsService.Get(testId));
        }

        [Test]
        public void Create_ReturnsCreatedProfessorId()
        {
            var newProfessor = new Professor()
            {
                Name = "Andrey Bessonov",
                Email = "a.b@university.com",
            };

            _professorsRepository.Setup(p => p.Create((It.IsAny<Professor>())))
           .Callback(new Action<Professor>(newProfessor =>
           {
               dynamic maxProfessorID = _professors.Last().Id;
               dynamic nextProfessorID = maxProfessorID + 1;
               newProfessor.Id = nextProfessorID;
               _professors.Add(newProfessor);
           })).Returns<Professor>(prof => prof.Id);

            var createProfessorId = _professorsService.Create(newProfessor);

            Assert.That(newProfessor, Is.EqualTo(_professors.Last()));
            Assert.That(createProfessorId, Is.EqualTo(_professors.Last().Id));
        }

        [Test]
        public void Edit_PassCorrectProfessor()
        {
            var testProfessor = _professors.First();
            _professorsRepository.Setup(p => p.Edit(It.IsAny<Professor>()))
               .Callback(new Action<Professor>(prof =>
               {
                   dynamic oldProfessor = _professors.Find(a => a.Id == prof.Id);
                   oldProfessor = prof;
               })).Returns(testProfessor.Id);

            var updatedProfessor = new Professor()
            {
                Id = testProfessor.Id,
                Name = "Andrey Bessonov",
                Email = "a.b@university.com",
            };
            _professorsService.Edit(updatedProfessor);
            Assert.That(testProfessor.Id, Is.EqualTo(1));
            Assert.That(testProfessor.Name, Is.EqualTo(_professors.First().Name));
        }

        [Test]
        public void Edit_PassWrongProfessor()
        {
            var testProfessor = new Professor() { Id = 0 };

            _professorsRepository.Setup(p => p.Edit(testProfessor)).Throws<ProfessorNotFoundException>();
            Assert.Throws(typeof(ProfessorNotFoundException), () => _professorsService.Edit(testProfessor));
        }

        [Test]
        public void Delete_RemoveProfessorById()
        {
            var testProfessor = _professors.Last();

            _professorsRepository.Setup(p => p.Delete(It.IsAny<int>()))
            .Callback(new Action<int>(Id =>
            {
                var professorToRemove =
                    _professors.Find(prof => prof.Id == Id);

                if (professorToRemove != null)
                    _professors.Remove(professorToRemove);
            })).Returns(1);

            int maxID = _professors.Max(a => a.Id);
            _professorsService.Delete(testProfessor.Id);
            Assert.That(maxID, Is.GreaterThan(_professors.Max(a => a.Id)));
        }

        [Test]
        public void Delete_RemoveLectureByWrongId()
        {
            var testLecture = new Professor() { Id = 0 };

            _professorsRepository.Setup(p => p.Delete(It.IsAny<int>())).Throws<ProfessorNotFoundException>();

            Assert.Throws(typeof(ProfessorNotFoundException), () => _professorsService.Delete(testLecture.Id));
        }
    }
}