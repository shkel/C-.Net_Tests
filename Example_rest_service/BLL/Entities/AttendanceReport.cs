using System;

namespace BLL.Entities
{
    public class AttendanceReport
    {
        public string CourseName { get; set; }
        public DateTime LessonTime { get; set; }

        public string StudentName { get; set; }

        public bool Attendance { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AttendanceReport report &&
                   CourseName == report.CourseName &&
                   LessonTime == report.LessonTime &&
                   StudentName == report.StudentName &&
                   Attendance == report.Attendance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CourseName, LessonTime, StudentName, Attendance);
        }

        public override string ToString()
        {
            return String.Format("[CourseName={0}; LessonTime={1}; StudentName={2}; Attendance={3}]", CourseName, LessonTime, StudentName, Attendance);
        }

        public AttendanceReport(string courseName, DateTime lessonTime, string studentName, bool attendance)
        {
            CourseName = courseName;
            LessonTime = lessonTime;
            StudentName = studentName;
            Attendance = attendance;
        }
    }
}
