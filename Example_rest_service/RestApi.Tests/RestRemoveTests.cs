using Xunit;
using RestTest;
using System.Threading.Tasks;
using Models.Entities;

namespace RestApi.Tests
{
    public class RestRemoveTests : IClassFixture<RestWebAppFactory<Startup>>
    {
        private readonly RestWebAppFactory<Startup> _factory;

        public RestRemoveTests(RestWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateCourseReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Course/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateUserReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/User/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateLessonReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Lesson/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateRoleReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Role/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateUserRoleReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/UserRole/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateTrainingReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Training/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
