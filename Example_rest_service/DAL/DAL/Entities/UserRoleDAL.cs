using DAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("user_roles")]
    public class UserRoleDAL : IAutoKey
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserDAL UserDAL { get; set; }

        public int RoleId { get; set; }
        public RoleDAL RoleDAL { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
