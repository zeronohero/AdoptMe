using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdoptMe
{
    public class Animal
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
        public string Image { get; set; }
        public DateTime Date_added { get; set; }
        public DateTime? AdoptedAt { get; set; }

        public Animal // WARNING: Database Insert only  
                      //dont need to implicitly insert an id  
            (
            string name,
            string species,
            string color,
            int age,
            string gender,
            string status,
            int admin_id,
            string image
            )
        {
            Name = name;
            Species = species;
            Color = color;
            Age = age;
            Gender = gender;
            Status = status;
            Admin_id = admin_id;
            Image = image;
        }


        public Animal(
             int animal_id,
             string name,
             string species,
             string color,
             int age,
             string gender,
             string status,
             int admin_id,
             int? adoptee_id,
             string image,
             DateTime created_at,
             DateTime? adopted_at)
        {
            Animal_id = animal_id;
            Name = name;
            Species = species;
            Color = color;
            Age = age;
            Gender = gender;
            Status = status;
            Admin_id = admin_id;
            Adoptee_id = adoptee_id;
            Image = image;
            Date_added = created_at;
            AdoptedAt = adopted_at;
        }

        public void SaveToDatabase()
        {
            string query = @"INSERT INTO Animal   
                   (name, species, color, age, gender, status, admin_id, image)  
                   VALUES (@name, @species, @color, @age, @gender, @status, @admin_id, @image)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                   new SqlParameter("@name", Name),
                   new SqlParameter("@species", Species),
                   new SqlParameter("@color", Color),
                   new SqlParameter("@age", Age),
                   new SqlParameter("@gender", Gender),
                   new SqlParameter("@status", Status),
                   new SqlParameter("@admin_id", Admin_id),
                   new SqlParameter("@image", Image),
            };
            DatabaseConnection.ExecuteNonQuery(query, parameters);
        }
        public static List<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();
            SqlParameter[] parameters = new SqlParameter[] { };
            string query = "SELECT animal_id, name, species, color, age, gender, status, admin_id, adoptee_id, image, created_at, adopted_at FROM Animal";
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
                    string image = reader.IsDBNull(9) ? null : reader.GetString(9);
                    DateTime created_at = reader.GetDateTime(10);
                    DateTime? adopted_at = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11);

                    animals.Add(new Animal(
                        animal_id, name, species, color, age, gender, status, admin_id, adoptee_id, image, created_at, adopted_at
                    ));
                }
            }
            return animals;
        }
    }
}

