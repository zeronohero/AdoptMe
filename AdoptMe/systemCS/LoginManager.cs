using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptMe
{
    internal static class LoginManager
    {
        public static User Authenticate(string email, string password)
        {
            string queryAdmin = "SELECT admin_id, name, number, address FROM Admin WHERE email = @email AND password = @password";

            SqlParameter[] adminParams = new SqlParameter[]
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", password)
            };

            using (SqlDataReader reader = DatabaseConnection.ExecuteReader(queryAdmin, adminParams))
            {
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string number = reader.GetString(2);
                    string address = reader.GetString(3);

                    return new Admin(id, email, password, name, number, address);
                }
            }

            string queryAdoptee = "SELECT adoptee_id, name, number, address FROM Adoptee WHERE email = @email AND password = @password";

            SqlParameter[] userParams = new SqlParameter[]
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", password)
            };

            using (SqlDataReader reader = DatabaseConnection.ExecuteReader(queryAdoptee, userParams))
            {
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string number = reader.GetString(2);
                    string address = reader.GetString(3);

                    return new Adoptee(id, email, password, name, number, address);
                }
            }
            return null;
        }
    }
}
