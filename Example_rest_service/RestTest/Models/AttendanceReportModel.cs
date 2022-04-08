using System;
using System.Runtime.Serialization;

namespace Models.Entities
{
    [DataContract]
    public class AttendanceReportModel
    {
        [DataMember]
        public string CourseName { get; set; }
        [DataMember]
        public DateTime LessonTime { get; set; }
        [DataMember]
        public string StudentName { get; set; }
        [DataMember]
        public bool Attendance { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AttendanceReportModel report &&
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

        public AttendanceReportModel(string courseName, DateTime lessonTime, string studentName, bool attendance)
        {
            CourseName = courseName;
            LessonTime = lessonTime;
            StudentName = studentName;
            Attendance = attendance;
        }
    }
}
