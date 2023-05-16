using DataAccess;
using DataAccess.Models;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.IntegrationTests
{
    [TestFixture]
    public class StudentAttendanceControllerTests
    {
        private ApplicationDbContext _context;
        private WebApplicationFactory<Startup> _webHost;

        [SetUp]
        public void Setup()
        {
            _webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("studentAttendances_db");
                    });
                });
            });

            _context = _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            // creates default data from DataAccess.DatabaseInitializer
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void Teardown()
        {
            _context.RemoveRange(_context.StudentAttendances);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetStudentAttendances_ShouldReturnAllStudentAttendances()
        {
            _context.RemoveRange(_context.StudentAttendances);
            _context.SaveChanges();

            // Arrange
            _context.StudentAttendances.Add(new StudentAttendanceDb 
            {
                LectureId = 1,
                StudentId = 1,
                IsPresent = true,
                HomeworkMark = 5
            });

            _context.StudentAttendances.Add(new StudentAttendanceDb 
            {
                LectureId = 1,
                StudentId = 2,
                IsPresent = true,
                HomeworkMark = 0
            });

            _context.SaveChanges();
            var expected = _context.StudentAttendances.ToList().Count;
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("api/studentAttendance");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            int studentAttendancesCount = JsonConvert.DeserializeObject<IEnumerable<StudentAttendance>>(response.Content.ReadAsStringAsync().Result).Count();
            Assert.That(studentAttendancesCount, Is.EqualTo(expected));

        }

        [Test]
        public async Task GetStudentAttendance_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testStudAtt = new StudentAttendanceDb
            {
                LectureId = 2,
                StudentId = 2,
                IsPresent = true,
                HomeworkMark = 5
            };
            _context.StudentAttendances.Add(testStudAtt);
            _context.SaveChanges();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/studentAttendance/{testStudAtt.LectureId}, { testStudAtt.StudentId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var actual = JsonConvert.DeserializeObject<StudentAttendance>(response.Content.ReadAsStringAsync().Result);
            Assert.That(testStudAtt.HomeworkMark, Is.EqualTo(actual.HomeworkMark));

        }

        [Test]
        public async Task GetStudentAttendance_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/studentAttendance/{0}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddStudentAttendance_PassCorrectData_ShouldReturnOk()
        {
            var initialStudAttendancesCount = _context.StudentAttendances.Count();
            var testStudAtt = new StudentAttendance
            {
                LectureId = 1,
                StudentId = 3,
                LectureName = "Physics",
                StudentName = "Sergei Sidorov",
                HomeworkMark = 5,
                isPresent = true,
            };
            if (_context.StudentAttendances.Find(testStudAtt.LectureId, testStudAtt.StudentId) is not null)
            {
                _context.Remove(_context.StudentAttendances.Where(sa => sa.LectureId == testStudAtt.LectureId && sa.StudentId == testStudAtt.StudentId).FirstOrDefault());
                _context.SaveChanges();
            }


            var httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testStudAtt), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("api/studentAttendance", stringContent);

            // Assert
            var actual = _context.StudentAttendances.Find(testStudAtt.LectureId, testStudAtt.StudentId);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialStudAttendancesCount, Is.EqualTo(_context.StudentAttendances.Count()));
            Assert.That(testStudAtt.HomeworkMark, Is.EqualTo(actual.HomeworkMark));
            Assert.That(testStudAtt.isPresent, Is.EqualTo(actual.IsPresent));

        }

        [Test]
        public async Task AddStudentAttendance_PassWrongData_ShouldReturnBadRequest()
        {
            // Arrange
            var testStudentAttendance = new StudentAttendance
            {
                LectureId = 0,
                StudentId = 0,
            };

            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testStudentAttendance), Encoding.UTF8, "application/json");

            // Act            
            var response = await httpClient.PostAsync("api/studentAttendance", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateStudentAttendance_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            if (_context.StudentAttendances.Count() == 0)
            {
                var testStudAtt = new StudentAttendanceDb
                {
                    LectureId = 1,
                    StudentId = 1,
                    HomeworkMark = 5,
                    IsPresent = true
                };
                _context.StudentAttendances.Add(testStudAtt);
                _context.SaveChanges();
                _context.Entry(testStudAtt).State = EntityState.Detached;
            }

            var updateStudAtt = new StudentAttendance
            {
                LectureId = 1,
                StudentId = 1,
                LectureName = "Physics",
                StudentName = "Vasya Pupkin",
                HomeworkMark = 1,
                isPresent = false
            };
            var httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateStudAtt), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/studentAttendance/{updateStudAtt.LectureId}, { updateStudAtt.StudentId}", stringContent);

            // Assert
            var actual = _context.StudentAttendances.Find(updateStudAtt.LectureId, updateStudAtt.StudentId);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(updateStudAtt.LectureId, Is.EqualTo(actual.LectureId));
            Assert.That(updateStudAtt.StudentId, Is.EqualTo(actual.StudentId));
            Assert.That(updateStudAtt.HomeworkMark, Is.EqualTo(actual.HomeworkMark));

        }

        [Test]
        public async Task UpdateStudentAttendance_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            var testStudAtt = new StudentAttendance
            {
                LectureId = 1,
                StudentId = 4,
                LectureName = "Physics",
                StudentName = "Petya Nakhimov",
                HomeworkMark = 5,
                isPresent = true,
            };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testStudAtt), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/studentAttendance/{testStudAtt.LectureId}, { testStudAtt.StudentId}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteStudentAttendance_PassCorrectId()
        {
            // Arrange
            var testStudAtt = new StudentAttendanceDb
            {
                LectureId = 1,
                StudentId = 1,
                IsPresent = true,
                HomeworkMark = 5
            };
            _context.StudentAttendances.Add(testStudAtt);
            _context.SaveChanges();
            _context.Entry(testStudAtt).State = EntityState.Detached;
            var initialStudentAttendancesCount = _context.StudentAttendances.Count();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/studentAttendance/{testStudAtt.LectureId}, { testStudAtt.StudentId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialStudentAttendancesCount, Is.GreaterThan(_context.StudentAttendances.Count()));
            var actual = _context.StudentAttendances.Where(sa => sa.LectureId == testStudAtt.LectureId && sa.StudentId == testStudAtt.StudentId).FirstOrDefault();
            Assert.That(actual, Is.Null);

        }

        [Test]
        public async Task DeleteStudentAttendance_PassWrongId()
        {
            // Arrange
            var testStudAtt = new StudentAttendanceDb
            {
                LectureId = 0,
                StudentId = 1,
                IsPresent = true,
                HomeworkMark = 5
            };
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/studentAttendance/{testStudAtt.LectureId}, { testStudAtt.StudentId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}