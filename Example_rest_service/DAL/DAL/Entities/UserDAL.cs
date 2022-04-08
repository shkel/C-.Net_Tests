using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("users")]
    public class UserDAL : IAutoKey
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public ICollection<UserRoleDAL> UserRolesDAL { get; set; }
        public ICollection<LessonDAL> LessonsDAL { get; set; }
        public ICollection<TrainingDAL> TrainingsDAL { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
