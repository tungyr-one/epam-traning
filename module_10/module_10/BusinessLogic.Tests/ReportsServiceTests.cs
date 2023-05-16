using BusinessLogic.BusinessLogic.Notifier;
using BusinessLogic.Exceptions;
using Domain;
using Domain.Domain.ServicesInterfaces;
using LecturesApp._BusinessLogic.ReportGeneration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestHelper;

namespace BusinessLogic.Tests
{
    public class ReportsServiceTests
    {
        private Mock<ILecturesRepository> _lecturesRepository = new();
        private Mock<IProfessorsRepository> _professorsRepository = new();
        private Mock<IStudentsRepository> _studentsRepository = new();
        private Mock<IStudentAttendancesRepository> _studentsAttendancesRepository = new();
        private Mock<IReportGenerator> _reportGenerator = new();
        private Mock<IAttendanceReportManager> _reportManager = new();
        private Mock<IAttendanceAnalyzer> _attendanceAnalyzer = new();
        private Mock<IStudyProgressAnalyzer> _studyProgressAnalyzer = new();
        private Mock<INotifyManager> _emailNotifyManager = new();
        private Mock<INotifyManager> _smsNotifyManager = new();

        private IReportsService _reportsService;

        private List<Lecture> _lectures;
        private List<Professor> _professors;
        private List<Student> _students;
        private List<StudentAttendance> _studentAttendances;

        [SetUp]
        public void Setup()
        {
            _lectures = DataInitializer.GetAllLectures();
            _professors = DataInitializer.GetAllProfessors();
            _students = DataInitializer.GetAllStudents();
            _studentAttendances = DataInitializer.GetAllStudentAttendances();
            _reportsService = new ReportsService(_lecturesRepository.Object,
                                                _professorsRepository.Object,
                                                _studentsRepository.Object,
                                                _studentsAttendancesRepository.Object,
                                                _reportGenerator.Object,
                                                _reportManager.Object,
                                                _attendanceAnalyzer.Object,
                                                _studyProgressAnalyzer.Object,
                                                _emailNotifyManager.Object,
                                                _smsNotifyManager.Object,
                                                NullLogger<ReportsService>.Instance);
        }

        [TearDown]
        public void TearDown()
        {
            _students = null;
        }

        [Test]
        public void GenerateLectureAttendanceReport_PassCorrectData()
        {
            var testLectures = _lectures;
            JsonReportGenerator generator = new();
            var expectedReport = generator.GenerateAttendanceReport(_lectures[0]);

            _lecturesRepository
                .Setup(lr => lr.GetAllIncludedEntities((It.IsAny<string>())))
                .Returns(_lectures);
            _reportManager
                .Setup(rm => rm.CreateReport((It.IsAny<List<Lecture>>())))
                .Returns(expectedReport);

            var actualReport = _reportsService.GenerateLectureAttendanceReport(_lectures[0].Name);
            Assert.That(actualReport, Is.EqualTo(expectedReport));
        }

        [Test]
        public void GenerateLectureAttendanceReport_PassWrongData()
        {
            List<Lecture> testLectures = null;
            _lecturesRepository
                .Setup(lr => lr.GetAllIncludedEntities((It.IsAny<string>())))
                .Returns(testLectures);

            Assert.Throws(typeof(ReportGenerationFailException), () => _reportsService.GenerateLectureAttendanceReport(_lectures[0].Name));
        }

        [Test]
        public void LectureAttendanceCheck_PassCorrectData()
        {
            _studentsAttendancesRepository
                .Setup(repo => repo.GetAllByLectureName(_lectures[0].Name))
                .Returns(_studentAttendances);

            _attendanceAnalyzer
                .Setup(an => an.AnalyseLectureAttendance(_studentAttendances))
                .Returns(_studentAttendances);

            _lecturesRepository
                .Setup(repo => repo.Get(_studentAttendances[0].LectureId))
                .Returns(_lectures[0]);

            _professorsRepository
                .Setup(repo => repo.GetByName(_lectures[0].ProfessorName))
                .Returns(_professors[0]);

            _emailNotifyManager
                .Setup(man => man.Notify(_students[0].Email, "test msg"));

            var actual = _reportsService.LectureAttendanceCheck(_lectures[0].Name);
            Assert.That(actual, Is.EqualTo(_studentAttendances));
        }

        [Test]
        public void LectureAttendanceCheck_PassWrongLectureName()
        {
            _studentAttendances.Clear();
            _studentsAttendancesRepository
                .Setup(repo => repo.GetAllByLectureName(_lectures[0].Name))
                .Returns(_studentAttendances);

            Assert.Throws(typeof(AttendanceAnalyseFail), () => _reportsService.LectureAttendanceCheck(_lectures[0].Name));
            _studentAttendances = DataInitializer.GetAllStudentAttendances();
        }

        [Test]
        public void LectureAttendanceCheck_PassWrongProfessorName()
        {
            Professor testProfessor = null;
            _studentsAttendancesRepository
                .Setup(repo => repo.GetAllByLectureName(_lectures[0].Name))
                .Returns(_studentAttendances);

            _attendanceAnalyzer
                .Setup(an => an.AnalyseLectureAttendance(_studentAttendances))
                .Returns(_studentAttendances);

            _lecturesRepository
                .Setup(repo => repo.Get(_studentAttendances[0].LectureId))
                .Returns(_lectures[0]);

            _professorsRepository
                .Setup(repo => repo.GetByName(_lectures[0].ProfessorName))
                .Returns(testProfessor);

            Assert.Throws(typeof(NotifyFail), () => _reportsService.LectureAttendanceCheck(_lectures[0].Name));
        }

        [Test]
        public void LectureStudyProgressCheck_PassCorrectName()
        {
            var testProblemAttendances = new List<Tuple<StudentAttendance, double>>() { new Tuple<StudentAttendance, double>(_studentAttendances[0], 3.5) };
            _studentsAttendancesRepository
                .Setup(repo => repo.GetAllByLectureName(_lectures[0].Name))
                .Returns(_studentAttendances);

            _studyProgressAnalyzer
                .Setup(an => an.AnalyseLectureStudy(_studentAttendances))
                .Returns(testProblemAttendances);

            _studentsRepository
                .Setup(repo => repo.GetByName(_studentAttendances[0].StudentName))
                .Returns(_students[0]);

            _smsNotifyManager
                .Setup(repo => repo.Notify(_students[0].Phone, "test msg"));

            var actual = _reportsService.LectureStudyProgressCheck(_lectures[0].Name);
            Assert.That(actual, Is.EqualTo(testProblemAttendances));
        }

        [Test]
        public void LectureStudyProgressCheck_PassWrongLectureName()
        {
            List<StudentAttendance> testAttendances = new();
            _studentsAttendancesRepository
                .Setup(repo => repo.GetAllByLectureName(_lectures[0].Name))
                .Returns(testAttendances);

            Assert.Throws(typeof(StudyProgressAnalyseFail), () => _reportsService.LectureStudyProgressCheck(_lectures[0].Name));
        }

        [Test]
        public void LectureStudyProgressCheck_GetWrongStudentData()
        {
            var testProblemAttendances = new List<Tuple<StudentAttendance, double>>() { new Tuple<StudentAttendance, double>(_studentAttendances[0], 3.5) };
            Student testStudent = null;
            _studentsAttendancesRepository
                .Setup(repo => repo.GetAllByLectureName(_lectures[0].Name))
                .Returns(_studentAttendances);

            _studyProgressAnalyzer
                .Setup(an => an.AnalyseLectureStudy(_studentAttendances))
                .Returns(testProblemAttendances);

            _studentsRepository
                .Setup(repo => repo.GetByName(_studentAttendances[0].StudentName))
                .Returns(testStudent);

            Assert.Throws(typeof(NotifyFail), () => _reportsService.LectureStudyProgressCheck(_lectures[0].Name));
        }

        [Test]
        public void GenerateStudentsMailList_PassCorrectStudentAttendancesList()
        {
            _studentsRepository
                .Setup(repo => repo.Get(_students[0].Id))
                .Returns(_students[0]);

            var actual = _reportsService.GenerateStudentsMailList(_studentAttendances);
            Assert.That(actual[0], Is.EqualTo(_students[0]));
        }

        // STUDENT REPORT
        [Test]
        public void GenerateStudentAttendanceReport_PassCorrectData()
        {
            var testStudent = _students[0];
            JsonReportGenerator generator = new();
            var expectedReport = generator.GenerateAttendanceReport(_students[0]);

            _studentsRepository
                .Setup(lr => lr.GetByName((testStudent.Name)))
                .Returns(testStudent);

            _reportManager
                .Setup(rm => rm.CreateReport((testStudent)))
                .Returns(expectedReport);

            var actualReport = _reportsService.GenerateStudentAttendanceReport(testStudent.Name);
            Assert.That(actualReport, Is.EqualTo(expectedReport));
        }

        [Test]
        public void GenerateStudentAttendanceReport_PassWrongData()
        {
            Student testStudent = null;
            _studentsRepository
                .Setup(lr => lr.GetByName((It.IsAny<string>())))
                .Returns(testStudent);

            Assert.Throws(typeof(ReportGenerationFailException), () => _reportsService.GenerateStudentAttendanceReport(_students[0].Name));
        }

        [Test]
        public void StudentAttendanceCheck_PassCorrectData()
        {
            _studentsRepository
                .Setup(repo => repo.GetByName(_students[0].Name))
                .Returns(_students[0]);

            _attendanceAnalyzer
                .Setup(an => an.AnalyseStudentAttendance(_students[0]))
                .Returns(_studentAttendances);

            _lecturesRepository
                .Setup(repo => repo.Get(_studentAttendances[0].LectureId))
                .Returns(_lectures[0]);

            _professorsRepository
                .Setup(repo => repo.GetByName(_lectures[0].ProfessorName))
                .Returns(_professors[0]);

            _emailNotifyManager
                .Setup(man => man.Notify(_students[0].Email, "test msg"));

            var actual = _reportsService.StudentAttendanceCheck(_students[0].Name);
            Assert.That(actual, Is.EqualTo(_studentAttendances));
        }

        [Test]
        public void StudentAttendanceCheck_PassWrongStudentName()
        {
            Student testStudent = null;
            _studentsRepository
                .Setup(repo => repo.GetByName(_students[0].Name))
                .Returns(testStudent);

            Assert.Throws(typeof(AttendanceAnalyseFail), () => _reportsService.StudentAttendanceCheck(_students[0].Name));
        }

        [Test]
        public void StudentStudyProgressCheck_PassCorrectName()
        {
            var testProblemAttendances = new List<Tuple<StudentAttendance, double>>() { new Tuple<StudentAttendance, double>(_studentAttendances[0], 3.5) };

            _studentsRepository
                .Setup(repo => repo.GetByName(_students[0].Name))
                .Returns(_students[0]);

            _studyProgressAnalyzer
                .Setup(an => an.AnalyseStudentStudy(It.IsAny<List<StudentAttendance>>()))
                .Returns(testProblemAttendances);

            _smsNotifyManager
                .Setup(repo => repo.Notify(_students[0].Phone, "test msg"));

            var actual = _reportsService.StudentStudyProgressCheck(_students[0].Name);
            Assert.That(actual, Is.EqualTo(testProblemAttendances));
        }

        [Test]
        public void StudentStudyProgressCheck_PassWrongStudentName()
        {
            Student testStudent = null;

            _studentsRepository
                .Setup(repo => repo.GetByName(_students[0].Name))
                .Returns(testStudent);

            Assert.Throws(typeof(StudyProgressAnalyseFail), () => _reportsService.StudentStudyProgressCheck(_students[0].Name));
            _studentsRepository.Verify();
        }

        [Test]
        public void GenerateProfessorsMailList_PassCorrectStudentAttendancesList()
        {
            _lecturesRepository
                .Setup(repo => repo.Get(_studentAttendances[0].LectureId))
                .Returns(_lectures[0]);

            _professorsRepository
                .Setup(repo => repo.GetByName(_lectures[0].ProfessorName))
                .Returns(_professors[0]);

            var actual = _reportsService.GenerateProfessorsMailList(_studentAttendances);
            Assert.That(actual[0], Is.EqualTo(_professors[0]));
        }
    }
}