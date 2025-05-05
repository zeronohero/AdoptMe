using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptMe
{
    internal class AdoptionRequest
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Color { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } // 'male' or 'female'
        public string Status { get; set; } // 'adopted' or 'not_adopted'
        public int Admin_id { get; set; } // Admin ID
        public int Adoptee_id { get; set; } // Adoptee ID or null

        public void SaveToDatabase()
        {
            string query = @"INSERT INTO Animal 
                (name, species, color, age, gender, status, admin_id, adoptee_id)
                VALUES (@name, @species, @color, @age, @gender, @status, @admin_id)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", Name),
                new SqlParameter("@species", Species),
                new SqlParameter("@color", Color),
                new SqlParameter("@age", Age),
                new SqlParameter("@gender", Gender),
                new SqlParameter("@status", Status),
                new SqlParameter("@admin_id", Admin_id),
            };

            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }

        public AdoptionRequest(string name, string species, string color, int age, string gender, string status, int admin_id)
        {
            Name = name;
            Species = species;
            Color = color;
            Age = age;
            Gender = gender;
            Status = status;
            this.Admin_id = admin_id;
        }
    }
}

