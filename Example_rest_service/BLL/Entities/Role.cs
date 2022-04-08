using System;

namespace BLL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Role role &&
                   Id == role.Id &&
                   Name == role.Name &&
                   Description == role.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description);
        }

        public override string ToString()
        {
            return String.Format("[ID={0}; Name={1}; Description={2}]", Id, Name, Description??"NULL");
        }
    }
}
