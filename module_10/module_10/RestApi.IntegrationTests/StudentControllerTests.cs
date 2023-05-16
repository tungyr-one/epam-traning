using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
using System.Xml.Linq;
using TestHelper;

namespace RestApi.IntegrationTests
{
    [TestFixture]
    public class StudentControllerTests
    {
        private ApplicationDbContext _context;
        private WebApplicationFactory<Startup> _webHost;

        [SetUp]
        public void Setup()
        {
            _context = null;
            _webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("students_db");
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
            _context.RemoveRange(_context.Students);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetStudents_ShouldReturnAllStudents()
        {
            // Arrange
            _context.Students.Add(new StudentDb
            {
                Name = "TestingName1",
                Email = "v.pupkin@university.com",
                Phone = "89543876435",
                Age = 18,
            });
            _context.Students.Add(new StudentDb 
            {
                Name = "TestingName2",
                Email = "i.ivanov@university.com",
                Phone = "8954388925378128676435",
                Age = 19,
            });
            _context.SaveChanges();
            var expected = _context.Students.Count();

            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("api/student");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            int studentsCount = JsonConvert.DeserializeObject<IEnumerable<Student>>(response.Content.ReadAsStringAsync().Result).Count();
            Assert.That(expected, Is.EqualTo(studentsCount));

        }

        [Test]
        public async Task GetStudent_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testStudent = new StudentDb { Id = 5, Name = "Testing", Email = "a.einstein@university.com" };
            _context.Students.Add(testStudent);
            _context.SaveChanges();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/student/{testStudent.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var actual = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().Result);
            Assert.That(testStudent.Name, Is.EqualTo(actual.Name));

        }

        [Test]
        public async Task GetStudent_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/student/{0}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddStudent_PassCorrectData_ShouldReturnOk()
        {
            // Arrange
            var initialStudentsCount = _context.Students.Count();
            var testStudent = new Student 
            {
                    Name = "Vasya Pupkin",
                    Email = "v.pupkin@university.com",
                    Phone = "89543876435",
                    Age = 18,
                    StudentAttendances = new List<StudentAttendance>()
            };

            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testStudent), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("api/student", stringContent);

            // Assert
            var lastStudent = _context.Students.Last();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialStudentsCount, Is.LessThan(_context.Students.Count()));
            Assert.That(testStudent.Name, Is.EqualTo(lastStudent.Name));
        }

        [Test]
        public async Task AddStudent_PassWrongData_ShouldReturnBadRequest()
        {
            // Arrange
            var testStudent = new Student { };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testStudent), Encoding.UTF8, "application/json");

            // Act            
            var response = await httpClient.PostAsync("api/student", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateStudent_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testStudent = new StudentDb 
            {
                Id = 9,                
                Name = "TestingName",
                Email = "v.pupkin@university.com",
                Phone = "89543876435",
                Age = 18,
            };
            _context.Students.Add(testStudent);
            _context.SaveChanges();

            var updateStudent = new Student 
            { 
                Id = 9,
                Name = "UpdatedTestingName",
                Email = "v.shmutkin@university.com",
                Phone = "89543876435",
                Age = 20,
                StudentAttendances = new List<StudentAttendance>() 
            };

            var httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateStudent), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/student/{testStudent.Id}", stringContent);

            // Assert
            var actual = _context.Students.Find(testStudent.Id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(testStudent.Name, Is.EqualTo(actual.Name));
            Assert.That(testStudent.Email, Is.EqualTo(actual.Email));
        }

        [Test]
        public async Task UpdateStudent_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            var testStudent = new Student
            {
                Id = 0,
                Name = "TestingName",
                Email = "v.pupkin@university.com",
                Phone = "89543876435",
                Age = 18,
                StudentAttendances = new List<StudentAttendance>()
            };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testStudent), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/student/{testStudent.Id}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Test]
        public async Task DeleteStudent_PassCorrectId()
        {
            // Arrange
            var testStudent = new StudentDb { Id = 8, Name = "TestingName", Email = "d.mendeleev@university.com"};
            _context.Students.Add(testStudent);
            _context.SaveChanges();
            _context.Entry(testStudent).State = EntityState.Detached;
            var initialStudentsCount = _context.Students.Count();

            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/student/{testStudent.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialStudentsCount, Is.GreaterThan(_context.Students.Count()));
            Assert.That(_context.Students.Find(testStudent.Id), Is.Null);

        }

        [Test]
        public async Task DeleteStudent_PassWrongId()
        {
            // Arrange
            var testStudent = new StudentDb { Id = 0 };
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/lecture/{testStudent.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}