using BLL.Entities;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;

namespace ConsoleApp1
{
    class ProgramBll
    {

        static void Main()
        {
            var options = new DbContextOptionsBuilder<DBContext>();
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 1)));

            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                using DBContext context = new(options.Options, true);
                using var provider = RegisterServices(context);
                InitializeData(provider);

                ReportsService reportService = provider.GetRequiredService<ReportsService>();
                Console.WriteLine("Attendance : Math at 01.06.2021");
                foreach (var item in reportService.GetAttendanceAsync("Math", new DateTime(2021, 6, 1)).Result)
                {
                    Console.WriteLine(item);
                };
                Console.WriteLine("student2");
                foreach (var item in reportService.GetAttendanceAsync("student", "2").Result)
                {
                    Console.WriteLine(item);
                };
                Console.WriteLine("Absenteeism");
                foreach (var item in reportService.SendAbsenteeismReportAsync("student", "2", "Math").Result)
                {
                    Console.WriteLine(item);
                };
                Console.WriteLine("Avg Score");
                Console.WriteLine(reportService.SendAVGUserScoreAsync(3, 1).Result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        private static ServiceProvider RegisterServices(DBContext context)
        {
            var config = new ConfigurationBuilder().Build();
            return new ServiceCollection()
            /*    .AddTransient<ITrainingService<Role>>(s => new RoleService(
                    s.GetRequiredService<IRepository<RoleDAL>>(),
                    s.GetRequiredService<ILogger<RoleService>>()
                ))
                .AddTransient<IRepository<RoleDAL>>(s => new Repository<RoleDAL>(context))

                .AddTransient<ITrainingService<User>>(s => new UserService(
                    s.GetRequiredService<IRepository<UserDAL>>(),
                    s.GetRequiredService<ILogger<UserService>>()
                ))
                .AddTransient<IRepository<UserDAL>>(s => new Repository<UserDAL>(context))

                .AddTransient<ITrainingService<UserRole>>(s => new UserRoleService(
                    s.GetRequiredService<IRepository<UserRoleDAL>>(),
                    s.GetRequiredService<ILogger<UserRoleService>>()
                ))
                .AddTransient<IRepository<UserRoleDAL>>(s => new Repository<UserRoleDAL>(context))

                .AddTransient<ITrainingService<Course>>(s => new CourseService(
                    s.GetRequiredService<IRepository<CourseDAL>>(),
                    s.GetRequiredService<ILogger<CourseService>>()
                ))
                .AddTransient<IRepository<CourseDAL>>(s => new Repository<CourseDAL>(context))

                .AddTransient<ITrainingService<Lesson>>(s => new LessonService(
                    s.GetRequiredService<IRepository<LessonDAL>>(),
                    s.GetRequiredService<ILogger<LessonService>>()
                ))
                .AddTransient<IRepository<LessonDAL>>(s => new Repository<LessonDAL>(context))

                .AddTransient<ITrainingService<Training>>(s => new TrainingService(
                    s.GetRequiredService<IRepository<TrainingDAL>>(),
                    s.GetRequiredService<ILogger<TrainingService>>()
                ))
                .AddTransient<IRepository<TrainingDAL>>(s => new Repository<TrainingDAL>(context))

                .AddTransient(s => new ReportsService(
                    s.GetRequiredService<IRepository<CourseDAL>>(),
                    s.GetRequiredService<IRepository<LessonDAL>>(),
                    s.GetRequiredService<IRepository<UserDAL>>(),
                    s.GetRequiredService<IRepository<TrainingDAL>>(),
                    s.GetRequiredService<ILogger<ReportsService>>()
                ))

                .AddLogging(loggingBuilder => loggingBuilder.AddNLog(config))*/
                .BuildServiceProvider();
        }

        private static void InitializeData(ServiceProvider provider)
        {
            ITrainingService<Role> roleService = provider.GetRequiredService<ITrainingService<Role>>();
            roleService.Create(new Role() { Name = "lector" });
            roleService.Create(new Role() { Name = "student" });
            //myService.Create(new Role() { Id = 2, Name = "student" }); // test error

            ITrainingService<User> userService = provider.GetRequiredService<ITrainingService<User>>();
            userService.Create(new User() { Email = "lector1@test.xx" });
            userService.Create(new User() { Email = "lector2@test.xx" });
            userService.Create(new User() { FirstName = "student", LastName = "1", Email = "student1@test.xx", Phone = "111" });
            userService.Create(new User() { FirstName = "student", LastName = "2", Email = "student2@test.xx", Phone = "222" });
            userService.Create(new User() { FirstName = "student", LastName = "3", Email = "student3@test.xx", Phone = "333" });

            ITrainingService<UserRole> userRoleService = provider.GetRequiredService<ITrainingService<UserRole>>();
            userRoleService.Create(new UserRole() { UserId = 1, RoleId = 1});
            userRoleService.Create(new UserRole() { UserId = 2, RoleId = 1 });
            userRoleService.Create(new UserRole() { UserId = 3, RoleId = 2 });
            userRoleService.Create(new UserRole() { UserId = 4, RoleId = 2 });
            userRoleService.Create(new UserRole() { UserId = 5, RoleId = 2 });

            ITrainingService<Course> courseService = provider.GetRequiredService<ITrainingService<Course>>();
            courseService.Create(new Course() { Name = "Math" });
            courseService.Create(new Course() { Name = "Music" });

            ITrainingService<Lesson> lessonService = provider.GetRequiredService<ITrainingService<Lesson>>();
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 1, LessonTime = new DateTime(2021, 6, 1) });
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 1, LessonTime = new DateTime(2021, 6, 3) });
            lessonService.Create(new Lesson() { CourseId = 1, LectorID = 1, LessonTime = new DateTime(2021, 6, 10) });
            lessonService.Create(new Lesson() { CourseId = 2, LectorID = 2, LessonTime = new DateTime(2021, 6, 2) });
            lessonService.Create(new Lesson() { CourseId = 2, LectorID = 2, LessonTime = new DateTime(2021, 6, 9) });
            lessonService.Create(new Lesson() { CourseId = 2, LectorID = 2, LessonTime = new DateTime(2021, 6, 16) });

            ITrainingService<Training> trainingService = provider.GetRequiredService<ITrainingService<Training>>();
            trainingService.Create(new Training() { LessonId = 1, StudentId = 3, Attendance = true, Grade = Training.Mark.Good });
            trainingService.Create(new Training() { LessonId = 1, StudentId = 4, Attendance = false, Grade = Training.Mark.Good }); // test mapper
            trainingService.Create(new Training() { LessonId = 1, StudentId = 5, Attendance = true, Grade = Training.Mark.Satisfactory });
            trainingService.Create(new Training() { LessonId = 2, StudentId = 3, Attendance = true, Grade = Training.Mark.Satisfactory });
            trainingService.Create(new Training() { LessonId = 2, StudentId = 4, Attendance = true, Grade = Training.Mark.LowFailure });
            trainingService.Create(new Training() { LessonId = 2, StudentId = 5, Attendance = true, Grade = Training.Mark.Satisfactory });
        }
    }
}
