using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ajax.Data
{
    public class PeopleRepository
    {
        private string _connectionString { get; set; }
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
            public List<Person> GetAll()
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM info";
                conn.Open();
                var reader = cmd.ExecuteReader();
                List<Person> ppl = new();
                while (reader.Read())
                {
                    ppl.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"],

                    });
                }

                return ppl;
            }

            public void AddPerson(Person person)
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO info (FirstName, LastName, Age) " +
                    "VALUES(@f, @l, @a) SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@f", person.FirstName);
                cmd.Parameters.AddWithValue("@l", person.LastName);
                cmd.Parameters.AddWithValue("@a", person.Age);
                conn.Open();
                person.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        public void DeletePerson(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from info Where Id=@id";
            cmd.Parameters.AddWithValue("@id", id);
           
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void EditPerson(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Info Set FirstName=@f, LastName=@l, Age=@age WHERE Id=@id"; 
        
            cmd.Parameters.AddWithValue("@f", person.FirstName);
            cmd.Parameters.AddWithValue("@l", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

    }



    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}


