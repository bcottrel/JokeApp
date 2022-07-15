using JokeApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JokeApp.Services
{
    public class JokeDAO : IJokeDataService
    {
        /* string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
         Initial Catalog=aspnet-JokeApp-3E3CAA9A-C513-46C7-8AAA-CD8CC4EF959F;
         Integrated Security=True;Connect Timeout=30;Encrypt=False;
         TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        */

        
        
        public void Delete(Joke joke, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string sqlStatement = "Delete FROM dbo.Joke WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", joke.Id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        } 
        
        //Returns All jokes from the database
        public List<Joke> GetAllJokes(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            List<Joke> foundJokes = new List<Joke>();

            string sqlStatement = "Select * FROM dbo.Joke";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundJokes.Add(new Joke
                        {
                            Id = (int)reader[0],
                            JokeQuestion = (string)reader[1],
                            JokeAnswer = (string)reader[2]
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return foundJokes;
        }
    

        
        //Search for a certain phrase in the joke's Question and Answer
        public List<Joke> SearchJokes(string search, IConfiguration configuration)
        {

            List<Joke> jokes = new List<Joke>();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string sqlStatement = "Select * FROM dbo.Joke WHERE JokeQuestion LIKE @JokeQuestion " +
                "OR JokeAnswer LIKE @JokeAnswer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@JokeQuestion", '%' + search + '%');
                command.Parameters.AddWithValue("@JokeAnswer", '%' + search + '%');


                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        jokes.Add(new Joke
                        {
                            Id = (int)reader[0],
                            JokeQuestion = (string)reader[1],
                            JokeAnswer = (string)reader[2]
                        });
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return jokes;
        }

        public int Insert(Joke joke, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            int newIdNumber = -1;

            string sqlStatement = "INSERT INTO dbo.Joke(JokeQuestion,JokeAnswer) " +
                "VALUES ( @JokeQuestion, @JokeAnswer )";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                

                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@JokeQuestion", joke.JokeQuestion);
                command.Parameters.AddWithValue("@JokeAnswer", joke.JokeAnswer);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception e)

                {
                    Console.WriteLine(e.Message);
                }
            }
            return newIdNumber;
        }

        public int Update(Joke joke, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            int newIdNumber = -1;

            string sqlStatement = "Update dbo.Joke SET " +
                "JokeQuestion = @JokeQuestion, " +
                "JokeAnswer = @JokeAnswer " +
                "WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@JokeQuestion", joke.JokeQuestion);
                command.Parameters.AddWithValue("@JokeAnswer", joke.JokeAnswer);
                command.Parameters.AddWithValue("@Id", joke.Id);

                try
                {
                    connection.Open();
                    newIdNumber = Convert.ToInt32(command.ExecuteScalar());
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return newIdNumber;
        } 

        public Joke FindId(int? id, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            Joke? jokes = null;

            string sqlStatement = "Select * FROM dbo.Joke WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        jokes = new Joke
                        {
                            Id = (int)reader[0],
                            JokeQuestion = (string)reader[1],
                            JokeAnswer = (string)reader[2]
                        };
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return jokes;
        }
    }
}
        