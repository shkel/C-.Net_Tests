using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
