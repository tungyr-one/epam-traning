using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace RestApi.IntegrationTests
{
    [TestFixture]
    public class BasicTests
    {
        private WebApplicationFactory<Startup> _factory;

        //public BasicTests(WebApplicationFactory<Startup> factory)
        //{
        //    _factory = factory;
        //}

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
        }



        //[TestCase("/")]
        [TestCase("/api/lecture")]
        [TestCase("/api/student")]
        [TestCase("/api/professor")]
        [TestCase("/api/studentAttendance")]
        //[TestCase("/")]
        //[TestCase("/Privacy")]
        //[TestCase("/Contact")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            //var db =_factory.Services.GetService<ApplicationDbContext>();
            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.That("application/json; charset=utf-8", Is.EqualTo(response.Content.Headers.ContentType.ToString()));
        }


    }
}
