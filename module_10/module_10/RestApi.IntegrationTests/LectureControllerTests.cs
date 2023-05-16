using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestApi.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace RestApi.IntegrationTests
{
    [TestFixture]
    public class LectureControllerTests
    {
        private ApplicationDbContext _context;
        private WebApplicationFactory<Startup> _webHost;

        [OneTimeSetUp]
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
                        options.UseInMemoryDatabase("lectures_db");
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
            _context.RemoveRange(_context.Lectures);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetLectures_ShouldReturnAllLectures()
        {
            // Arrange
            _context.Lectures.Add(new LectureDb 
            {
                Name = "Testing", 
                Date = new System.DateTime(2021, 10, 1),
                ProfessorId = 1 
            });

            _context.Lectures.Add(new LectureDb 
            {
                Name = "AnotherTesting", 
                Date = new System.DateTime(2021, 09, 15), 
                ProfessorId = 1 
            });

            _context.SaveChanges();
            var expected = _context.Lectures.Count();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("api/lecture");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            int lecturesCount = JsonConvert.DeserializeObject<IEnumerable<Lecture>>(response.Content.ReadAsStringAsync().Result).Count();
            Assert.That(expected, Is.EqualTo(lecturesCount));

        }

        [Test]
        public async Task GetLecture_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testLecture = new LectureDb 
            { 
                Id = 5, 
                Name = "Testing", 
                Date = new System.DateTime(2021, 10, 1), 
                ProfessorId = 1 
            };
            _context.Lectures.Add(testLecture);
            _context.SaveChanges();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/lecture/{testLecture.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var actual = JsonConvert.DeserializeObject<Lecture>(response.Content.ReadAsStringAsync().Result);
            Assert.That(testLecture.Name, Is.EqualTo(actual.Name));

        }

        [Test]
        public async Task GetLecture_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/lecture/{0}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddLecture_PassCorrectData_ShouldReturnOk()
        {
            // Arrange
            var initialLecturesCount = _context.Lectures.Count();
            var testLecture = new Lecture 
            { 
                Name = "Testing", 
                Date = new System.DateTime(2021, 10, 1), 
                ProfessorName = "A. Ivanov", 
                StudentAttendances = new List<StudentAttendance>() 
            };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testLecture), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("api/lecture", stringContent);

            // Assert
            var lastLecture = _context.Lectures.Last();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialLecturesCount, Is.LessThan(_context.Lectures.Count()));
            Assert.That(testLecture.Name, Is.EqualTo(lastLecture.Name));
        }

        [Test]
        public async Task AddLecture_PassWrongData_ShouldReturnBadRequest()
        {
            // Arrange
            var testLecture = new Lecture 
            { 
                Date = new System.DateTime(2021, 10, 1), 
                ProfessorName = "A. Ivanov", 
                StudentAttendances = new List<StudentAttendance>() 
            };

            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testLecture), Encoding.UTF8, "application/json");
            
            // Act            
            var response = await httpClient.PostAsync("api/lecture", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateLecture_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testLecture = new LectureDb 
            { 
                Id = 9, 
                Name = "Testing", 
                Date = new System.DateTime(1980, 11, 25), 
                ProfessorId = 1, 
            };
            _context.Lectures.Add(testLecture);
            _context.SaveChanges();

            var updateLecture = new Lecture 
            { 
                Id = 9, 
                Name = "UpdateTesting", 
                Date = new System.DateTime(2000, 11, 25), 
                ProfessorName = "A. Petrov", 
                StudentAttendances = new List<StudentAttendance>() 
            };
            var httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateLecture), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/lecture/{testLecture.Id}", stringContent);

            // Assert
            var actual = _context.Lectures.Find(testLecture.Id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(testLecture.Name, Is.EqualTo(actual.Name));
            Assert.That(testLecture.Date, Is.EqualTo(actual.Date));
        }

        [Test]
        public async Task UpdateLecture_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            var testLecture = new Lecture 
            { 
                Id = 0, 
                Name = "UpdateTesting", 
                Date = new System.DateTime(1980, 11, 25), 
                ProfessorName = "A. Petrov", 
                StudentAttendances = new List<StudentAttendance>() 
            };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testLecture), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/lecture/{testLecture.Id}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public async Task DeleteLecture_PassCorrectId()
        {
            // Arrange
            var testLecture = new LectureDb 
            { 
                Id = 8, 
                Name = "Testing", 
                Date = new System.DateTime(1980, 11, 25), 
                ProfessorId = 1 
            };
            _context.Lectures.Add(testLecture);
            _context.SaveChanges();
            _context.Entry(testLecture).State = EntityState.Detached;
            var initialLecturesCount = _context.Lectures.Count();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/lecture/{testLecture.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialLecturesCount, Is.GreaterThan(_context.Lectures.Count()));
            Assert.That(_context.Lectures.Find(testLecture.Id), Is.Null);

        }
        
        [Test]
        public async Task DeleteLecture_PassWrongId()
        {
            // Arrange
            var testLecture = new LectureDb 
            { 
                Id = 0, 
                Name = "Testing", 
                Date = new System.DateTime(1980, 11, 25), 
                ProfessorId = 1, 
            };
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/lecture/{testLecture.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}