using Xunit;
using RestTest;
using System.Threading.Tasks;
using Models.Entities;
using FluentAssertions;
using static RestApi.Tests.Utils;

namespace RestApi.Tests
{
    public class RestSetTests : IClassFixture<RestWebAppFactory<Startup>>
    {
        private readonly RestWebAppFactory<Startup> _factory;

        public RestSetTests(RestWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddCourseReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new CourseModel() { Id = 30, Name = "OOP" };

            // Act
            var response = await client.PostAsync("/api/Course", MakeHttpContent(expected));
            CourseModel courses = await Utils.ReadAsJsonAsync<CourseModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            expected.Should().BeEquivalentTo(courses);
        }

        [Fact]
        public async Task AddUserReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new UserModel() { Id = 10, Email = "test@test.test", FirstName = "test", LastName = "1" };

            // Act
            var response = await client.PostAsync("/api/User", MakeHttpContent(expected));
            UserModel res = await Utils.ReadAsJsonAsync<UserModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            expected.Should().BeEquivalentTo(res);
        }

        [Fact]
        public async Task AddLessonReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new LessonModel() { Id = 10, CourseId = 1, LectorID = 4, LessonTime = new System.DateTime(2021, 6, 25) };

            // Act
            var response = await client.PostAsync("/api/Lesson", MakeHttpContent(expected));
            LessonModel res = await Utils.ReadAsJsonAsync<LessonModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            expected.Should().BeEquivalentTo(res);
        }

        [Fact]
        public async Task AddRoleReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new RoleModel() { Id = 30, Name = "Admin" };

            // Act
            var response = await client.PostAsync("/api/Role", MakeHttpContent(expected));
            RoleModel res = await Utils.ReadAsJsonAsync<RoleModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            expected.Should().BeEquivalentTo(res);
        }

        [Fact]
        public async Task AddUserRoleReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new UserRoleModel() { Id = 30, RoleId = 1, UserId = 5 };

            // Act
            var response = await client.PostAsync("/api/UserRole", MakeHttpContent(expected));
            UserRoleModel courses = await Utils.ReadAsJsonAsync<UserRoleModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            expected.Should().BeEquivalentTo(courses);
        }

        [Fact]
        public async Task AddTrainingReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new TrainingModel() { Id = 30, LessonId = 7, StudentId = 5, Attendance = true, HomeWorkMark = TrainingModel.Grade.LowFailure };

            // Act
            var response = await client.PostAsync("/api/Training", MakeHttpContent(expected));
            TrainingModel res = await Utils.ReadAsJsonAsync<TrainingModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            expected.Should().BeEquivalentTo(res);
        }
    }
}
