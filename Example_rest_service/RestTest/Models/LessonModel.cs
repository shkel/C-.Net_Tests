using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class LessonModel
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }
        [Required]
        public int LectorID { get; set; }

        public DateTime LessonTime { get; set; }
    }
}
