using System;

namespace BLL.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int LectorID { get; set; }

        public DateTime LessonTime { get; set; }
    }
}
