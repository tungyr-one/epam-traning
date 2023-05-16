using BusinessLogic.BusinessLogic.Notifier;
using BusinessLogic.Exceptions;
using Domain;
using Domain.Domain.ServicesInterfaces;
using LecturesApp._BusinessLogic.ReportGeneration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BusinessLogic.Tests")]

namespace BusinessLogic
{
    internal class ReportsService : IReportsService
    {
        private readonly ILecturesRepository _lecturesRepository;
        private readonly IProfessorsRepository _professorsRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IStudentAttendancesRepository _studentsAttendancesRepository;
        private readonly IReportGenerator _reportGenerator;

        private readonly IAttendanceReportManager _reportManager;
        private readonly IAttendanceAnalyzer _attendanceAnalyzer;
        private readonly IStudyProgressAnalyzer _studyProgressAnalyzer;
        private readonly INotifyManager _emailNotifyManager;
        private readonly INotifyManager _smsNotifyManager;
        private readonly ILogger _logger;

        public ReportsService()
        {
        }

        public ReportsService(ILecturesRepository lecturesRepository,
            IProfessorsRepository professorsRepository,
            IStudentsRepository studentsRepository,
            IStudentAttendancesRepository studentsAttendancesRepository,
            IReportGenerator reportGenerator,
            IAttendanceReportManager reportManager,
            IAttendanceAnalyzer attendanceAnalyzer,
            IStudyProgressAnalyzer studyProgressAnalyzer,
            INotifyManager emailNotifyManager,
            INotifyManager smsNotifyManager,
            ILogger<ReportsService> logger)
        {
            _lecturesRepository = lecturesRepository;
            _professorsRepository = professorsRepository;
            _studentsRepository = studentsRepository;
            _studentsAttendancesRepository = studentsAttendancesRepository;
            _reportGenerator = reportGenerator;

            _reportManager = reportManager;
            _attendanceAnalyzer = attendanceAnalyzer;
            _studyProgressAnalyzer = studyProgressAnalyzer;
            _emailNotifyManager = emailNotifyManager;
            _smsNotifyManager = smsNotifyManager;
            _logger = logger;

            _reportManager.SetGenerator(_reportGenerator);
            _emailNotifyManager.SetMessageSender(new EmailSender());
            _smsNotifyManager.SetMessageSender(new SmsSender());
        }

        public string GenerateLectureAttendanceReport(string lectureName)
        {
            if (_lecturesRepository.GetAllIncludedEntities(lectureName) is IEnumerable<Lecture> lectures)
            {
                return _reportManager.CreateReport(lectures);
            }
            else
            {
                _logger.LogError($"Unable get report of lecture with name {lectureName}");
                throw new ReportGenerationFailException("Unable to generate report.");
            }
        }

        public List<StudentAttendance> LectureAttendanceCheck(string lectureName)
        {
            var attendances = _studentsAttendancesRepository.GetAllByLectureName(lectureName).ToList();
            if (attendances == null || attendances.Count == 0)
            {
                _logger.LogError($"Unable to check attendances of lecture with name {lectureName}");
                throw new AttendanceAnalyseFail("Unable to check lecture attendances");
            }

            var problemAttendances = _attendanceAnalyzer.AnalyseLectureAttendance(attendances);

            if (problemAttendances.Count > 0)
            {
                var studMailList = GenerateStudentsMailList(problemAttendances);

                if (_professorsRepository.GetByName(_lecturesRepository.Get(attendances.First().LectureId).ProfessorName) is Professor professor && professor != null)
                {
                    foreach (var stud in studMailList)
                    {
                        _emailNotifyManager.Notify(stud.Email, $"Dear student {stud.Name}. You are lazybones!");
                        _emailNotifyManager.Notify(professor.Email, $"Dear professor {professor.Name}. Your student {stud.Name} is lazybones!");
                    }
                }
                else
                {
                    _logger.LogError($"Unable get professor of lecture with name {lectureName}");
                    throw new NotifyFail("Unable to notify by email");
                }
            }
            return problemAttendances;
        }

        public List<Tuple<StudentAttendance, double>> LectureStudyProgressCheck(string lectureName)
        {
            var attendances = _studentsAttendancesRepository.GetAllByLectureName(lectureName).ToList();
            if (attendances == null || attendances.Count == 0)
            {
                _logger.LogError($"Unable to check study progress of lecture with name {lectureName}");
                throw new StudyProgressAnalyseFail("Unable to check lecture study progress");
            }
            var problemAttendances = _studyProgressAnalyzer.AnalyseLectureStudy(attendances);

            if (problemAttendances.Count > 0)
            {
                foreach (var lectureAtt in problemAttendances)
                {
                    if (_studentsRepository.GetByName(lectureAtt.Item1.StudentName) is Student student && student != null)
                    {
                        _smsNotifyManager.Notify(student.Phone, $"Dear student {student.Name}. Your {lectureAtt.Item1.LectureName} average mark is {lectureAtt.Item2:0.0}!");
                    }
                    else
                    {
                        _logger.LogError($"Unable get student of lecture {lectureName}");
                        throw new NotifyFail("Unable to notify by email");
                    }
                }
            }
            return problemAttendances;
        }

        public List<Student> GenerateStudentsMailList(List<StudentAttendance> attendances)
        {
            List<Student> studentsSendList = new();
            foreach (var att in attendances)
            {
                var student = _studentsRepository.Get(att.StudentId);
                if (student is not null)
                    studentsSendList.Add(student);
            }
            return studentsSendList;
        }

        // STUDENT REPORT

        public string GenerateStudentAttendanceReport(string studentName)
        {
            if (_studentsRepository.GetByName(studentName) is Student student)
            {
                return _reportManager.CreateReport(student);
            }
            else
            {
                _logger.LogError($"Unable get report of student with name {studentName}");
                throw new ReportGenerationFailException("Unable to generate report.");
            }
        }

        public List<StudentAttendance> StudentAttendanceCheck(string studentName)
        {
            var student = _studentsRepository.GetByName(studentName);
            if (student is null)
            {
                _logger.LogError($"Unable to check attendances of student {studentName}");
                throw new AttendanceAnalyseFail("Unable to check student attendances");
            }

            var problemAttendances = _attendanceAnalyzer.AnalyseStudentAttendance(student);

            if (problemAttendances.Count > 0)
            {
                var profMailList = GenerateProfessorsMailList(problemAttendances);

                foreach (var prof in profMailList)
                {
                    if (prof is null)
                    {
                        _logger.LogError($"Unable get professor of student {studentName}");
                        throw new NotifyFail("Unable to notify by email");
                    }
                    else
                    {
                        _emailNotifyManager.Notify(prof.Email, $"Dear professor {prof.Name}. Your student {student.Name} is lazybones!");
                    }
                }
                _emailNotifyManager.Notify(student.Email, $"Dear student {student.Name}. You are lazybones!");
            }
            return problemAttendances;
        }

        public List<Tuple<StudentAttendance, double>> StudentStudyProgressCheck(string studentName)
        {
            var student = _studentsRepository.GetByName(studentName);
            if (student is null)
            {
                _logger.LogError($"Unable to check study progress of student {studentName}");
                throw new StudyProgressAnalyseFail("Unable to check student study progress");
            }

            var problemAttendances = _studyProgressAnalyzer.AnalyseStudentStudy(student.StudentAttendances);

            if (problemAttendances.Count > 0)
            {
                foreach (var lectureAtt in problemAttendances)
                {
                    _smsNotifyManager.Notify(student.Phone, $"Dear student {student.Name}. Your {lectureAtt.Item1.LectureName} average mark is {lectureAtt.Item2:0.0}!");
                }
            }
            return problemAttendances;
        }

        public List<Professor> GenerateProfessorsMailList(List<StudentAttendance> attendances)
        {
            List<Professor> professorsSendList = new();
            foreach (var att in attendances)
            {
                var professor = _professorsRepository.GetByName(_lecturesRepository.Get(att.LectureId).ProfessorName);
                professorsSendList.Add(professor);
            }
            return professorsSendList;
        }
    }
}