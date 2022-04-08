using DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("roles")]
    public class RoleDAL : IAutoKey
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRoleDAL> UserRolesDAL { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
