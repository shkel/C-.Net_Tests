using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using static DAL.Entities.TrainingDAL;

namespace ConsoleApp1
{
    class ProgramDal
    {

        /*static void Main()
        {
            var options = new DbContextOptionsBuilder<DBContext>();
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 1)));
            //var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection2"].ConnectionString;
            //options.UseNpgsql(connection);

            using DBContext context = new(options.Options, true);
            InitializeTables(context);

            Repository<UserDAL> usersRepository = new(context);

            var users = usersRepository.GetAll();
            foreach (UserDAL u in users)
                Console.WriteLine($"{u.Id}.{u.FirstName} - {u.LastName}");
        }*/

        private static void InitializeTables(DBContext context)
        {
            Repository<RoleDAL> rolesRepository = new(context);
            var lectorRole = rolesRepository.Create(new RoleDAL() { Name = "lector", Description = "has a salary" });
            var studentRole = rolesRepository.Create(new RoleDAL() { Name = "student", Description = "pays tuition" });

            Repository<CourseDAL> coursesRepository = new(context);
            var math = coursesRepository.Create(new CourseDAL() { Name = "Math" });
            var english = coursesRepository.Create(new CourseDAL() { Name = "English" });

            Repository<UserDAL> usersRepository = new(context);
            var s1 = usersRepository.Create(new UserDAL() { Email = "student1@test.com", FirstName = "Student", LastName = "1" });
            var s2 = usersRepository.Create(new UserDAL() { Email = "student2@test.com", FirstName = "Student", LastName = "2" });
            var s3 = usersRepository.Create(new UserDAL() { Email = "student3@test.com", FirstName = "Student", LastName = "3" });
            var lector1 = usersRepository.Create(new UserDAL() { Email = "lector1@test.com", FirstName = "Lector", LastName = "Tech 1" });
            var lector2 = usersRepository.Create(new UserDAL() { Email = "lector2@test.com", FirstName = "Lector", LastName = "Tech 2" });
            var lector3 = usersRepository.Create(new UserDAL() { Email = "lector3@test.com", FirstName = "Lector", LastName = "Social 1" });

            Repository<UserRoleDAL> userRolesRepository = new(context);
            userRolesRepository.Create(new UserRoleDAL() { RoleDAL = lectorRole, UserDAL = lector1 });
            userRolesRepository.Create(new UserRoleDAL() { RoleDAL = lectorRole, UserDAL = lector2 });
            userRolesRepository.Create(new UserRoleDAL() { RoleDAL = studentRole, UserDAL = s1 });
            userRolesRepository.Create(new UserRoleDAL() { RoleDAL = studentRole, UserDAL = s2 });
            userRolesRepository.Create(new UserRoleDAL() { RoleDAL = studentRole, UserDAL = s3 });

            Repository<LessonDAL> lessonsRepository = new(context);
            var l1 = lessonsRepository.Create(new LessonDAL() { CourseDAL = math, UserDAL = lector1, LessonTime = new DateTime(2021, 6, 1) });
            var l2 = lessonsRepository.Create(new LessonDAL() { CourseDAL = math, UserDAL = lector1, LessonTime = new DateTime(2021, 6, 4) });
            var l3 = lessonsRepository.Create(new LessonDAL() { CourseDAL = math, UserDAL = lector2, LessonTime = new DateTime(2021, 6, 8) });
            var l4 = lessonsRepository.Create(new LessonDAL() { CourseDAL = math, UserDAL = lector1, LessonTime = new DateTime(2021, 6, 11) });
            var l5 = lessonsRepository.Create(new LessonDAL() { CourseDAL = math, UserDAL = lector2, LessonTime = new DateTime(2021, 6, 15) });
            var l6 = lessonsRepository.Create(new LessonDAL() { CourseDAL = english, UserDAL = lector3, LessonTime = new DateTime(2021, 6, 2) });
            var l7 = lessonsRepository.Create(new LessonDAL() { CourseDAL = english, UserDAL = lector3, LessonTime = new DateTime(2021, 6, 9) });
            var l8 = lessonsRepository.Create(new LessonDAL() { CourseDAL = english, UserDAL = lector3, LessonTime = new DateTime(2021, 6, 10) });

            Repository<TrainingDAL> trainingsRepository = new(context);
            trainingsRepository.Create(new TrainingDAL() { Lesson = l1, Student = s1, Attendance = true, Grade = Score.Good });
            trainingsRepository.Create(new TrainingDAL() { Lesson = l1, Student = s2, Attendance = true, Grade = Score.Failure });
            trainingsRepository.Create(new TrainingDAL() { Lesson = l1, Student = s3, Attendance = false, Grade = Score.Failure });
        }


        /* private static void InitializeTables(DBContext context)
         {
             var s1 = context.Users.Add(new UserDAL() {Email = "student1@test.com", FirstName = "Student", LastName = "1" });
             var s2 = context.Users.Add(new UserDAL() { Email = "student2@test.com", FirstName = "Student", LastName = "2" });
             var s3 = context.Users.Add(new UserDAL() { Email = "student3@test.com", FirstName = "Student", LastName = "3" });
             var lector1 = context.Users.Add(new UserDAL() { Email = "lector1@test.com", FirstName = "Lector", LastName = "Tech 1" });
             var lector2 = context.Users.Add(new UserDAL() { Email = "lector2@test.com", FirstName = "Lector", LastName = "Tech 2" });
             var lector3 = context.Users.Add(new UserDAL() { Email = "lector3@test.com", FirstName = "Lector", LastName = "Social 1" });

             var lectorRole = context.Roles.Add(new RoleDAL() { Name = "lector", Description = "has a salary" });
             var studentRole = context.Roles.Add(new RoleDAL() { Name = "student", Description = "pays tuition" });

             var math = context.Courses.Add(new CourseDAL() { Name = "Math"});
             var english = context.Courses.Add(new CourseDAL() { Name = "English" });

             context.UserRoles.Add(new UserRoleDAL() { RoleDAL = lectorRole.Entity, UserDAL = lector1.Entity });
             context.UserRoles.Add(new UserRoleDAL() { RoleDAL = lectorRole.Entity, UserDAL = lector2.Entity });
             context.UserRoles.Add(new UserRoleDAL() { RoleDAL = studentRole.Entity, UserDAL = s1.Entity });
             context.UserRoles.Add(new UserRoleDAL() { RoleDAL = studentRole.Entity, UserDAL = s2.Entity });
             context.UserRoles.Add(new UserRoleDAL() { RoleDAL = studentRole.Entity, UserDAL = s3.Entity });

             var l1 = context.Lessons.Add(new LessonDAL() { CourseDAL = math.Entity, UserDAL = lector1.Entity, LessonTime = new DateTime(2021, 6, 1) });
             var l2 = context.Lessons.Add(new LessonDAL() { CourseDAL = math.Entity, UserDAL = lector1.Entity, LessonTime = new DateTime(2021, 6, 4) });
             var l3 = context.Lessons.Add(new LessonDAL() { CourseDAL = math.Entity, UserDAL = lector2.Entity, LessonTime = new DateTime(2021, 6, 8) });
             var l4 = context.Lessons.Add(new LessonDAL() { CourseDAL = math.Entity, UserDAL = lector1.Entity, LessonTime = new DateTime(2021, 6, 11) });
             var l5 = context.Lessons.Add(new LessonDAL() { CourseDAL = math.Entity, UserDAL = lector2.Entity, LessonTime = new DateTime(2021, 6, 15) });
             var l6 = context.Lessons.Add(new LessonDAL() { CourseDAL = english.Entity, UserDAL = lector3.Entity, LessonTime = new DateTime(2021, 6, 2) });
             var l7 = context.Lessons.Add(new LessonDAL() { CourseDAL = english.Entity, UserDAL = lector3.Entity, LessonTime = new DateTime(2021, 6, 9) });
             var l8 = context.Lessons.Add(new LessonDAL() { CourseDAL = english.Entity, UserDAL = lector3.Entity, LessonTime = new DateTime(2021, 6, 10) });

             context.Trainings.Add(new TrainingDAL() { Lesson = l1.Entity, Student = s1.Entity, Attendance = true, Grade = Score.Good });
             context.Trainings.Add(new TrainingDAL() { Lesson = l1.Entity, Student = s2.Entity, Attendance = true, Grade = Score.Failure });
             context.Trainings.Add(new TrainingDAL() { Lesson = l1.Entity, Student = s3.Entity, Attendance = false, Grade = Score.Failure });

             context.SaveChanges();
         }*/
    }
}
