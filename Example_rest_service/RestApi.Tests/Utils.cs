using System.Text.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BLL.Interfaces;
using BLL.Entities;

namespace RestApi.Tests
{
    public static class Utils
    {
        public static void InitializeDbForTests(IServiceProvider provider)
        {
            ITrainingService<Role> roleService = provider.GetRequiredService<ITrainingService<Role>>();
            roleService.Create(new Role() { Name = "lector" });
            roleService.Create(new Role() { Name = "student" });

            ITrainingService<User> userService = provider.GetRequiredService<ITrainingService<User>>();
            userService.Create(new User() { FirstName = "Student", LastName = "1", Email = "student1@test.com" });
            userService.Create(new User() { FirstName = "Student", LastName = "2", Email = "student2@test.com" });
            userService.Create(new User() { FirstName = "Student", LastName = "3", Email = "student3@test.com" });
            userService.Create(new User() { Email = "lector1@test.com", FirstName = "Lector", LastName = "Tech 1" });
            userService.Create(new User() { Email = "lector2@test.com", FirstName = "Lector", LastName = "Tech 2" });
            userService.Create(new User() { Email = "lector3@test.com", FirstName = "Lector", LastName = "Social 1" });


            ITrainingService<UserRole> userRoleService = provider.GetRequiredService<ITrainingService<UserRole>>();
            userRoleService.Create(new UserRole() { UserId = 1, RoleId = 2 });
            userRoleService.Create(new UserRole() { UserId = 2, RoleId = 2 });
            userRoleService.Create(new UserRole() { UserId = 3, RoleId = 2 });
            userRoleService.Create(new UserRole() { UserId = 4, RoleId = 1 });
            userRoleService.Create(new UserRole() { UserId = 5, RoleId = 1 });
            userRoleService.Create(new UserRole() { UserId = 6, RoleId = 1 });

            ITrainingService<Course> courseService = provider.GetRequiredService<ITrainingService<Course>>();
            courseService.Create(new Course() { Name = "Math" });
            courseService.Create(new Course() { Name = "English" });

            ITrainingService<Lesson> lessonService = provider.GetRequiredService<ITrainingService<Lesson>>();
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 4, LessonTime = new DateTime(2021, 6, 1) });
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 4, LessonTime = new DateTime(2021, 6, 4) });
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 5, LessonTime = new DateTime(2021, 6, 8) });
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 4, LessonTime = new DateTime(2021, 6, 11) });
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 5, LessonTime = new DateTime(2021, 6, 15) });
            lessonService.Create(new Lesson() { CourseId = 2, LectorID = 6, LessonTime = new DateTime(2021, 6, 2) });
            lessonService.Create(new Lesson() { CourseId = 2, LectorID = 6, LessonTime = new DateTime(2021, 6, 9) });
            lessonService.Create(new Lesson() { CourseId = 2, LectorID = 6, LessonTime = new DateTime(2021, 6, 10) });

            ITrainingService<Training> trainingService = provider.GetRequiredService<ITrainingService<Training>>();
            trainingService.Create(new Training() { LessonId = 1, StudentId = 1, Attendance = true, Grade = Training.Mark.Good });
            trainingService.Create(new Training() { LessonId = 1, StudentId = 2, Attendance = true, Grade = Training.Mark.Failure });
            trainingService.Create(new Training() { LessonId = 1, StudentId = 3, Attendance = false, Grade = Training.Mark.Failure });
            trainingService.Create(new Training() { LessonId = 2, StudentId = 3, Attendance = false, Grade = Training.Mark.Failure });
            trainingService.Create(new Training() { LessonId = 4, StudentId = 3, Attendance = false, Grade = Training.Mark.Failure });
        }

        public static async Task<T> ReadAsJsonAsync<T>(HttpContent httpContent)
        {
            using var stream = await httpContent.ReadAsStreamAsync();
            var rawData = new StreamReader(stream).ReadToEnd();
            var deserializeOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            deserializeOptions.Converters.Add(new DateTimeJsonConverter());
            return JsonSerializer.Deserialize<T>(rawData, deserializeOptions);
        }

        private class DateTimeJsonConverter : JsonConverter<DateTime>
        {
            private static string format = "yyyy-MM-ddThh:mm:ss";
            public override DateTime Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
                return DateTime.ParseExact(reader.GetString(), format, CultureInfo.InvariantCulture);
            }

            public override void Write(
                Utf8JsonWriter writer,
                DateTime dateTimeValue,
                JsonSerializerOptions options)
            {
                writer.WriteStringValue(dateTimeValue.ToString(format, CultureInfo.InvariantCulture));
            }
        }

        public static StringContent MakeHttpContent<T>(T expected)
        {
            var payload = JsonSerializer.Serialize(expected);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }
}
