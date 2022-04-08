using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class RoleModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
