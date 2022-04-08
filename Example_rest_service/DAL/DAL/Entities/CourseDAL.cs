using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("courses")]
    public class CourseDAL : IAutoKey
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<LessonDAL> LessonsDAL { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
