using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("lessons")]
    public class LessonDAL : IAutoKey
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public CourseDAL CourseDAL { get; set; }

        public int LectorID { get; set; }
        public virtual UserDAL UserDAL { get; set; }

        [Required]
        public DateTime LessonTime { get; set; }

        public ICollection<TrainingDAL> TrainingsDAL { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
