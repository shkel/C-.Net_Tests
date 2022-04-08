using Xunit;
using RestTest;
using System.Threading.Tasks;
using Models.Entities;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace RestApi.Tests
{
    public class RestGetTests : IClassFixture<RestWebAppFactory<Startup>>
    {
        private readonly RestWebAppFactory<Startup> _factory;

        public RestGetTests(RestWebAppFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_CoursesReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new List<CourseModel>() {
                 new CourseModel(){ Id = 1, Name = "Math"},
                 new CourseModel(){ Id = 2, Name = "English"}
            };

            // Act
            var response = await client.GetAsync("/api/Course");
            List<CourseModel> courses = await Utils.ReadAsJsonAsync<List<CourseModel>>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(courses);

        }

        [Fact]
        public async Task Get_CourseByIdReturnCorrectValue()
        {   
            using var client = _factory.CreateClient();
            var expected = new CourseModel() { Id = 1, Name = "Math" };

            // Act
            var response = await client.GetAsync("/api/Course/1");
            CourseModel courses = await Utils.ReadAsJsonAsync<CourseModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(courses);

        }

        [Fact]
        public async Task Get_UsersReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new List<UserModel>() {
                 new UserModel(){ Id = 1, Email = "student1@test.com", FirstName = "Student", LastName = "1"},
                 new UserModel(){ Id = 2, Email = "student2@test.com", FirstName = "Student", LastName = "2"},
                 new UserModel(){ Id = 3, Email = "student3@test.com", FirstName = "Student", LastName = "3"},
                 new UserModel(){ Id = 4, Email = "lector1@test.com", FirstName = "Lector", LastName = "Tech 1"},
                 new UserModel(){ Id = 5, Email = "lector2@test.com", FirstName = "Lector", LastName = "Tech 2"},
                 new UserModel(){ Id = 6, Email = "lector3@test.com", FirstName = "Lector", LastName = "Social 1"},
            };
            
            // Act
            var response = await client.GetAsync("/api/User");
            List<UserModel> users = await Utils.ReadAsJsonAsync<List<UserModel>>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(users);

        }

        [Fact]
        public async Task Get_UserByIdReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new UserModel() { Id = 1, Email = "student1@test.com", FirstName = "Student", LastName = "1" };

            // Act
            var response = await client.GetAsync("/api/User/1");
            UserModel courses = await Utils.ReadAsJsonAsync<UserModel>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(courses);

        }


        [Fact]
        public async Task Get_AttendanceByStudentReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new List<AttendanceReportModel>() { new AttendanceReportModel("Math", new DateTime(2021, 6, 1), "Student 1", true) };

            // Act
            var response = await client.GetAsync("api/Attendance/reportByStudent?studentFirstName=Student&studentLastName=1");
            List<AttendanceReportModel> report = await Utils.ReadAsJsonAsync<List<AttendanceReportModel>> (response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(report);

        }

        [Fact]
        public async Task Get_AttendanceByCourseReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new List<AttendanceReportModel>() {
                new AttendanceReportModel("Math", new DateTime(2021, 6, 1), "Student 1", true),
                new AttendanceReportModel("Math", new DateTime(2021, 6, 1), "Student 2", true),
                new AttendanceReportModel("Math", new DateTime(2021, 6, 1), "Student 3", false),
            };
     
            // Act
            var response = await client.GetAsync("api/Attendance/reportByCourse?courseName=Math&lessonTime=2021-06-01");
            List<AttendanceReportModel> report = await Utils.ReadAsJsonAsync<List<AttendanceReportModel>>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(report);

        }

        [Fact]
        public async Task Get_AbsenteeismByCourseReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            var expected = new List<AbsenteeismReportModel>() {
                new AbsenteeismReportModel("lector1@test.com", "student3@test.com"),
            };

            // Act
            var response = await client.GetAsync("/api/AbsenteeismReport/reportByStudentByCourse?studentFirstName=Student&studentLastName=3&courseName=Math");
            List<AbsenteeismReportModel> report = await Utils.ReadAsJsonAsync<List<AbsenteeismReportModel>>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().BeEquivalentTo(report);

        }

        [Fact]
        public async Task Get_AbsenteeismByStudentReturnCorrectValue()
        {
            // Arrange
            using var client = _factory.CreateClient();
            decimal expected = 4;

            // Act
            var response = await client.GetAsync("api/AbsenteeismReport/reportAvg?studentId=1&courseId=1");
            decimal report = await Utils.ReadAsJsonAsync<decimal>(response.Content);

            // Assert
            response.EnsureSuccessStatusCode(); 
            expected.Should().Be(report);

        }
    }
}
