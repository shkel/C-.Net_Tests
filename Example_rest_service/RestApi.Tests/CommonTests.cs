using Xunit;
using RestTest;
using System.Threading.Tasks;

namespace RestApi.Tests
{
    public class CommonTests : IClassFixture<RestWebAppFactory<Startup>>
    {
        private readonly RestWebAppFactory<Startup> _factory;

        public CommonTests(RestWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Course")]
        [InlineData("/api/Lesson")]
        [InlineData("/api/Role")]
        [InlineData("/api/User")]
        [InlineData("/api/UserRole")]
        [InlineData("/api/Training")]
        [InlineData("/api/Attendance/reportByStudent")]
        [InlineData("/api/Attendance/reportByCourse")]
        [InlineData("/api/AbsenteeismReport/reportByStudentByCourse")]
        [InlineData("/api/AbsenteeismReport/reportAvg")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/api/Course")]
        [InlineData("/api/Lesson")]
        [InlineData("/api/Role")]
        [InlineData("/api/User")]
        [InlineData("/api/UserRole")]
        [InlineData("/api/Training")]
        public async Task Get_EndpointsReturnCorrectContentType(string url)
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert            
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
