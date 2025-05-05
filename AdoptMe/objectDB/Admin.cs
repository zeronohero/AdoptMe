using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoptMe
{
    internal class Admin : User
    {
        public Admin(int id, string email, string password, string name, string number, string address) :
            base(id, email, password, Role.Admin, name, number, address){} 

        public void SaveToDatabase(string phone, string address)
        {
            string query = @"INSERT INTO Admin (name, email, password, number, address) 
                             VALUES (@name, @email, @password, @phone, @address)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", this.Name),
                new SqlParameter("@email", this.Email),
                new SqlParameter("@password", this.Password), // Hash before this in real use
                new SqlParameter("@phone", phone),
                new SqlParameter("@address", address)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
