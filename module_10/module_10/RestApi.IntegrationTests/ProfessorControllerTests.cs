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
    public class ProfessorControllerTests
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
                        options.UseInMemoryDatabase("professors_db");
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
            _context.RemoveRange(_context.Professors);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetProfessors_ShouldReturnAllProfessors()
        {
            // Arrange
            _context.Professors.Add(new ProfessorDb { Name = "TestingName", Email = "a.einstein@university.com" });
            _context.Professors.Add(new ProfessorDb { Name = "AnotherTestingName", Email = "d.mendeleev@university.com" });
            _context.SaveChanges();
            var expected = _context.Professors.Count();

            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync("api/professor");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            int professorsCount = JsonConvert.DeserializeObject<IEnumerable<Professor>>(response.Content.ReadAsStringAsync().Result).Count();
            Assert.That(expected, Is.EqualTo(professorsCount));

        }

        [Test]
        public async Task GetProfessor_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testProfessor = new ProfessorDb { Id = 5, Name = "Testing", Email = "a.einstein@university.com" };
            _context.Professors.Add(testProfessor);
            _context.SaveChanges();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/professor/{testProfessor.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var actual = JsonConvert.DeserializeObject<Professor>(response.Content.ReadAsStringAsync().Result);
            Assert.That(testProfessor.Name, Is.EqualTo(actual.Name));

        }

        [Test]
        public async Task GetProfessor_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            HttpResponseMessage response = await httpClient.GetAsync($"api/professor/{0}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddProfessor_PassCorrectData_ShouldReturnOk()
        {
            // Arrange
            var initialProfessorsCount = _context.Professors.Count();
            var testProfessor = new Professor { Name = "Testing", Email = "a.einstein@university.com", Lectures = new List<Lecture>() };

            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testProfessor), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("api/professor", stringContent);

            // Assert
            var lastProfessor = _context.Professors.Last();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialProfessorsCount, Is.LessThan(_context.Professors.Count()));
            Assert.That(testProfessor.Name, Is.EqualTo(lastProfessor.Name));
        }

        [Test]
        public async Task AddProfessor_PassWrongData_ShouldReturnBadRequest()
        {
            // Arrange
            var testProfessor = new Professor { };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testProfessor), Encoding.UTF8, "application/json");

            // Act            
            var response = await httpClient.PostAsync("api/professor", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateProfessor_PassCorrectId_ShouldReturnOk()
        {
            // Arrange
            var testProfessor = new ProfessorDb { Id = 9, Name = "TestingName", Email = "a.einstein@university.com" };
            _context.Professors.Add(testProfessor);
            _context.SaveChanges();
            var updateProfessor = new Professor { Id = 9, Name = "UpdateTestingName", Email = "d.mendeleev@university.com", Lectures = new List<Lecture>() };
            var httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateProfessor), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/professor/{testProfessor.Id}", stringContent);

            // Assert
            var actual = _context.Professors.Find(testProfessor.Id);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(testProfessor.Name, Is.EqualTo(actual.Name));
            Assert.That(testProfessor.Email, Is.EqualTo(actual.Email));

        }

        [Test]
        public async Task UpdateProfessor_PassWrongId_ShouldReturnNotFound()
        {
            // Arrange
            var testProfessor = new Professor { Id = 0, Name = "UpdateTestingName", Email = "d.mendeleev@university.com", Lectures = new List<Lecture>() };
            HttpClient httpClient = _webHost.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(testProfessor), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"api/professor/{testProfessor.Id}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteProfessor_PassCorrectId()
        {
            // Arrange
            var testProfessor = new ProfessorDb { Id = 8, Name = "TestingName", Email = "d.mendeleev@university.com"};
            _context.Professors.Add(testProfessor);
            _context.SaveChanges();
            _context.Entry(testProfessor).State = EntityState.Detached;
            var initialProfessorsCount = _context.Professors.Count();
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/professor/{testProfessor.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(initialProfessorsCount, Is.GreaterThan(_context.Professors.Count()));
            Assert.That(_context.Professors.Find(testProfessor.Id), Is.Null);

        }

        [Test]
        public async Task DeleteProfessor_PassWrongId()
        {
            // Arrange
            var testProfessor = new ProfessorDb { Id = 0};
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            var response = await httpClient.DeleteAsync($"api/professor/{testProfessor.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}