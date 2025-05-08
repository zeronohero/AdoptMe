using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptMe
{
    internal class User
    {
        private Role admin;

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public enum Role
        {
            Admin,
            Adoptee
        }
        public Role IsRole { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public User(int id, string email, string password, Role isRole, string name, string number, string address)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
            this.IsRole = isRole;
            this.Name = name;
            this.Number = number;
        }
    }

}
