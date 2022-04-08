using BLL.Entities;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ReportsService
    {
        private IRepository<CourseDAL> CourseRepository { get; set; }
        private IRepository<LessonDAL> LessonRepository { get; set; }
        private IRepository<UserDAL> UserRepository { get; set; }
        private IRepository<TrainingDAL> TrainingRepository { get; set; }
        private readonly ILogger<ReportsService> _logger;
        private readonly ITrainigEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public ReportsService(
            IRepository<CourseDAL> courseRepository,
            IRepository<LessonDAL> lessonRepository,
            IRepository<UserDAL> userRepository,
            IRepository<TrainingDAL> trainingRepository,
            ITrainigEmailSender emailSender,
            ISmsSender smsSender,
        ILogger<ReportsService> logger
        )
        {
            if (courseRepository == null)
            {
                throw new TrainingException("Course Repository is Null");
            }
            if (lessonRepository == null)
            {
                throw new TrainingException("Lesson Repository is Null");
            }
            if (userRepository == null)
            {
                throw new TrainingException("User Repository is Null");
            }
            if (trainingRepository == null)
            {
                throw new TrainingException("Training Repository is Null");
            }
            if (logger == null)
            {
                throw new TrainingException("Logger is Null");
            }
            if (emailSender == null)
            {
                throw new TrainingException("EmailSender is Null");
            }
            if (smsSender == null)
            {
                throw new TrainingException("SmsSender is Null");
            }
            CourseRepository = courseRepository;
            LessonRepository = lessonRepository;
            UserRepository = userRepository;
            TrainingRepository = trainingRepository;
            _emailSender = emailSender;
            _logger = logger;
            _smsSender = smsSender;
    }
        public async Task<IEnumerable<AttendanceReport>> GetAttendanceAsync(string courseName, DateTime lessonTime)
        {
            try
            {
                var coursesTask = CourseRepository.GetAllAsync();
                var lessonsTask = LessonRepository.GetAllAsync();
                var trainingsTask = TrainingRepository.GetAllAsync();
                var usersTask = UserRepository.GetAllAsync();
                await Task.WhenAll(coursesTask, lessonsTask, trainingsTask, usersTask);

                int courseId = coursesTask.Result.First(item => item.Name.Equals(courseName)).Id;
                int lessonId = lessonsTask.Result.First(item => item.CourseId == courseId && DateTime.Compare(item.LessonTime, lessonTime) == 0).Id;

                return trainingsTask.Result
                    .Join(
                        usersTask.Result,
                        attendance => attendance.StudentId,
                        user => user.Id,
                        (attendance, user) => new { user.FirstName, user.LastName, attendance.Attendance, attendance.LessonId}
                    )
                    .Where(attendance => attendance.LessonId == lessonId)
                    .Select(attendance => new AttendanceReport(courseName, lessonTime, attendance.FirstName + " " + attendance.LastName, attendance.Attendance));
                    
            }
            catch (ArgumentNullException e)
            {
                throw new TrainingException("Course is null", e);
            }

            catch (InvalidOperationException e)
            {
                throw new TrainingException("Wrong course", e);
            }
        }

        public async Task<IEnumerable<AttendanceReport>> GetAttendanceAsync(string studentFirstName, string studentLastName)
        {
            try
            {
                var coursesTask = CourseRepository.GetAllAsync();
                var lessonsTask = LessonRepository.GetAllAsync();
                var trainingsTask = TrainingRepository.GetAllAsync();
                var usersTask = UserRepository.GetAllAsync();
                await Task.WhenAll(coursesTask, lessonsTask, trainingsTask, usersTask);
                int userId = usersTask.Result.First(item => item.FirstName.Equals(studentFirstName) && item.LastName.Equals(studentLastName)).Id;

                return from training in trainingsTask.Result
                       join lesson in lessonsTask.Result on training.LessonId equals lesson.Id
                        join course in coursesTask.Result on lesson.CourseId equals course.Id
                        where training.StudentId == userId
                       select new AttendanceReport(
                            course.Name, lesson.LessonTime, studentFirstName + " " + studentLastName, training.Attendance
                        );
                  }
            catch (ArgumentNullException e)
            {
                throw new TrainingException("User is null", e);
            }

            catch (InvalidOperationException e)
            {
                throw new TrainingException("Wrong user", e);
            }
        }

        private async Task<IEnumerable<AbsenteeismReport>> GetAbsenteeismAsync(string studentFirstName, string studentLastName, string courseName, int numberOfAbsence)
        {
            try
            {
                var trainingsTask = TrainingRepository.GetAllAsync();
                var lessonsTask = LessonRepository.GetAllAsync();
                var studentsTask = UserRepository.GetAllAsync();
                var lectorsTask = UserRepository.GetAllAsync();
                var coursesTask = CourseRepository.GetAllAsync();
                await Task.WhenAll(coursesTask, lessonsTask, trainingsTask, studentsTask, lectorsTask);

                return from training in trainingsTask.Result
                       join lesson in lessonsTask.Result on training.LessonId equals lesson.Id
                       join course in coursesTask.Result on lesson.CourseId equals course.Id
                       join lector in lectorsTask.Result on lesson.LectorID equals lector.Id
                       join student in studentsTask.Result on training.StudentId equals student.Id
                       where student.FirstName.Equals(studentFirstName)
                                && student.LastName.Equals(studentLastName)
                                && course.Name.Equals(courseName)
                                && !training.Attendance
                       group new { lector, student } by new AbsenteeismReport( lector.Email, student.Email) into gr
                       where gr.Count() == numberOfAbsence
                       select new AbsenteeismReport(gr.Key.LectorEmail, gr.Key.StudentEmail);
            }
            catch (ArgumentNullException e)
            {
                throw new TrainingException("Course is null", e);
            }

            catch (InvalidOperationException e)
            {
                throw new TrainingException("Wrong course", e);
            }
        }

        private async Task<decimal> GetAvgStudentScoreAsync(int studentId, int courseId)
        {
            try
            {
                var trainingsTask = TrainingRepository.GetAllAsync();
                var lessonsTask = LessonRepository.GetAllAsync();
                await Task.WhenAll(lessonsTask, trainingsTask);
                if (trainingsTask == null || lessonsTask == null)
                {
                    throw new TrainingException("Data is not found");
                }
                return trainingsTask.Result
                    .Join(
                        lessonsTask.Result,
                        training => training.LessonId,
                        lesson => lesson.Id,
                        (training, lesson) => new { training.Grade, lesson.CourseId, training.StudentId }
                    )
                    .Where(row => row.CourseId == courseId && row.StudentId == studentId)
                    .Select(row => (decimal) row.Grade)
                    .Average();
            }
            catch (InvalidOperationException e)
            {
                throw new TrainingException("Data is not found", e);
            }
            catch (ArgumentNullException e)
            {
                throw new TrainingException("Course is null", e);
            }
            catch (OverflowException e)
            {
                throw new TrainingException("Wrong avg rating", e);
            }
        }


        public async Task<IEnumerable<AbsenteeismReport>> SendAbsenteeismReportAsync(string studentFirstName, string studentLastName, string courseName)
        {
            int numberOfAbsence = 3;
            var res = await GetAbsenteeismAsync(studentFirstName, studentLastName, courseName, numberOfAbsence);

            try
            {
                await Task.WhenAll(res.Select(r =>
                    Task.Run(async () =>
                    {
                        await _emailSender.SendNotificationToStudent(r.StudentEmail, studentFirstName + " " + studentLastName);
                        await _emailSender.SendNotificationToLector(r.LectorEmail, "lector");
                    })
                ));
                return res;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAbsenteeismAsync");
                throw new TrainingException("Can't get report", e);
            }
        }

        public async Task<decimal> SendAVGUserScoreAsync(int studentId, int courseId)
        {
            try
            {
                var avgScore = await GetAvgStudentScoreAsync(studentId, courseId);
                if (avgScore < 4.0m)
                {
                    _smsSender.SendSMSNotification("", courseId.ToString() + ". Your GPA is below 4");
                }
                return avgScore;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAvgStudentScoreAsync");
                throw new TrainingException("Can't get report", e);
            }
        }


    }
}
