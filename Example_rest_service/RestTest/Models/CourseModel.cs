using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class CourseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
