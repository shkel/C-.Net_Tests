using System;

namespace BLL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Email == user.Email &&
                   FirstName == user.FirstName &&
                   LastName == user.LastName &&
                   Phone == user.Phone;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email, FirstName, LastName, Phone);
        }

        public override string ToString()
        {
            return String.Format("[ID={0}; Email={1}; FirstName={2}; LastName={3}; Phone = {4}]", Id, Email, FirstName, LastName, Phone);
        }
    }
}
