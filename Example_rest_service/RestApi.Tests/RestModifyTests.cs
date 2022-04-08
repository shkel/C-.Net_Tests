using Xunit;
using RestTest;
using System.Threading.Tasks;
using Models.Entities;
using static RestApi.Tests.Utils;

namespace RestApi.Tests
{
    public class RestModifyTests : IClassFixture<RestWebAppFactory<Startup>>
    {
        private readonly RestWebAppFactory<Startup> _factory;

        public RestModifyTests(RestWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateCourseReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new CourseModel() { Id = 1, Name = "OOP" };

            // Act
            var response = await client.PutAsync("/api/Course", MakeHttpContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task UpdateUserReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new UserModel() { Id = 1, Email = "test@test.test", FirstName = "test", LastName = "1" };

            // Act
            var response = await client.PutAsync("/api/User", MakeHttpContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task UpdateLessonReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new LessonModel() { Id = 1, CourseId = 1, LectorID = 4, LessonTime = new System.DateTime(2021, 6, 25) };

            // Act
            var response = await client.PutAsync("/api/Lesson", MakeHttpContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task UpdateRoleReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new RoleModel() { Id = 1, Name = "Admin" };

            // Act
            var response = await client.PutAsync("/api/Role", MakeHttpContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task UpdateUserRoleReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new UserRoleModel() { Id = 1, RoleId = 1, UserId = 5 };

            // Act
            var response = await client.PutAsync("/api/UserRole", MakeHttpContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); 
        }

        [Fact]
        public async Task UpdateTrainingReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new TrainingModel() { Id = 1, LessonId = 7, StudentId = 5, Attendance = true, HomeWorkMark = TrainingModel.Grade.LowFailure };

            // Act
            var response = await client.PutAsync("/api/Training", MakeHttpContent(expected));

            // Assert
            response.EnsureSuccessStatusCode(); 
        }
    }
}
