using Microsoft.Extensions.Configuration;
using StudentApi.Models;
using System.Data.SqlClient;

namespace StudentApi
{
   
   
    public class StudentRepo : IStudentRepo
    {
        IConfiguration _config;
        public StudentRepo(IConfiguration config)
        {

            _config = config;   
        }
        SqlConnection con = null;
        SqlCommand com = null;
        string GetConnectionString()
        {
            return _config.GetConnectionString("MyConnectionString").ToString();
        }
        SqlConnection GetConnection()
        {
            con = new SqlConnection(GetConnectionString());
            return con;
        }
        public Student AddStudent(Student student)
        {
            using (con = GetConnection())
            {
                using (com = new SqlCommand())
                {
                    com.CommandText = "Insert into student(name, address, course, marks) values (@name , @address, @course, @marks)";
                    com.Parameters.AddWithValue("@name", student.Name);
                    com.Parameters.AddWithValue("@address", student.Address);
                    com.Parameters.AddWithValue("@course", student.Course);
                    com.Parameters.AddWithValue("@marks", student.Marks);
                    com.Connection = con;
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
            return student;
        }

        public int DeleteStudent(int id)
        {
            using (con = GetConnection())
            {
                using (com = new SqlCommand())
                {
                    com.CommandText = "Delete from student where id = @id";
                    com.Parameters.AddWithValue("@id", id);
                    com.Connection = con;
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
            return id;
        }

        public Student EditStudent(Student student, int id)
        {
            using (con = GetConnection())
            {
                using (com = new SqlCommand())
                {
                    com.CommandText = "update student set address = @address, marks=@marks where id = @id";
                   
                    com.Parameters.AddWithValue("@address", student.Address);
                   
                    com.Parameters.AddWithValue("@marks", student.Marks);
                    com.Parameters.AddWithValue("@id", id);

                    com.Connection = con;
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
            return student;
        }

        public Student GetStudentById(int id)
        {
            Student student = null;
            using (con = GetConnection())
            {
                using (com = new SqlCommand())
                {
                    com.CommandText = "Select * from student where id = @id";
                    com.Parameters.AddWithValue("@id", id);

                    com.Connection = con;
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Read();
                        student = new Student()
                        {
                            Id = (int)reader[0],
                            Name = reader["name"].ToString(),
                            Address = reader["address"].ToString(),
                            Course = reader["course"].ToString(),
                            Marks = (int)reader["marks"]
                        };

                    }
                    con.Close();
                }
            }
            return student;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = null;
            using (con = GetConnection())
            {
                using (com = new SqlCommand())
                {
                    com.CommandText = "Select * from student ";
                    com.Connection = con;
                    con.Open();
                    SqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if(students==null)
                        {
                            students = new List<Student>();
                        }
                        while (reader.Read())
                        {
                            var student = new Student()
                            {
                                Id = (int)reader[0],
                                Name = reader["name"].ToString(),
                                Address = reader["address"].ToString(),
                                Course = reader["course"].ToString(),
                                Marks = (int)reader["marks"]
                            };
                            students.Add(student);
                        }
                    }
                    con.Close();
                }
            }
            return students;
        }
    }
}
