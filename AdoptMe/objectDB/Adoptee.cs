using System.Data.SqlClient;

namespace AdoptMe
{
    internal class Adoptee : User
    {
        public Adoptee(int id, string email, string password, string name, string number, string address)
        : base(id, email, password, Role.Adoptee, name, number, address) { }

        public Adoptee(string email, string password, string name, string number, string address)
        : base(email, password, Role.Adoptee, name, number, address) { }

        public void SaveToDatabase()
        {
            string query = @"INSERT INTO Adoptee (name, email, password, number, address)
                             VALUES (@name, @email, @password, @phone, @address)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", this.Name),
                new SqlParameter("@email", this.Email),
                new SqlParameter("@password", this.Password),
                new SqlParameter("@phone", this.Number),
                new SqlParameter("@address", this.Address)
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }
        public static string GetUserNameById(int userId)
        {
            string query = "SELECT name FROM [Adoptee] WHERE adoptee_id = @id";
            SqlParameter[] parameters = { new SqlParameter("@id", userId) };
            object result = DatabaseConnection.ExecuteScalar(query, parameters);
            return result != null ? result.ToString() : null;
        }
    }
}
