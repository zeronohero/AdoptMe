using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptMe
{
    internal class Animal
    {
        public int Animal_id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Color { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } // 'male' or 'female'
        public string Status { get; set; } // 'adopted' or 'not_adopted'
        public int Admin_id { get; set; } // Admin ID
        public int? Adoptee_id { get; set; } // Adoptee ID or null

        public Animal // WARNING: Database Insert only 
            //dont need to implicitly insert an id
            (
            string name, 
            string species, 
            string color, 
            int age, 
            string gender, 
            string status, 
            int admin_id
            )
        {
            Name = name;
            Species = species;
            Color = color;
            Age = age;
            Gender = gender;
            Status = status;
            Admin_id = admin_id;
        }

        public Animal // Complete Detail
            (
            int animal_id,
            string name,
            string species,
            string color,
            int age,
            string gender,
            string status,
            int admin_id,
            int ? adoptee_id
            )
        {
            Animal_id = animal_id;
            Name = name;
            Species = species;
            Color = color;
            Age = age;
            Gender = gender;
            Status = status;
            Admin_id = admin_id;
            Adoptee_id = admin_id;
        }

        public void SaveToDatabase()
        {
            string query = @"INSERT INTO Animal 
                (name, species, color, age, gender, status, admin_id)
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
        public static List<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();
            SqlParameter[] parameters = new SqlParameter[]{};
            string query = "SELECT animal_id, name, species, color, age, gender, status, admin_id, adoptee_id FROM Animal";
            using (SqlDataReader reader = DatabaseConnection.ExecuteReader(query, parameters))
            {
                while (reader.Read())
                {
                    int animal_id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string species = reader.GetString(2);
                    string color = reader.GetString(3);
                    int age = reader.GetInt32(4);
                    string gender = reader.GetString(5);
                    string status = reader.GetString(6);
                    int admin_id = reader.GetInt32(7);
                    int? adoptee_id = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8);
                    animals.Add(new Animal(animal_id, name, species, color, age, gender, status, admin_id, adoptee_id));
                }
            }
            return animals;
        }
    }
}

