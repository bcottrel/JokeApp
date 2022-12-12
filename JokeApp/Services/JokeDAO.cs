using JokeApp.Models;
using System.Data.SqlClient;

namespace JokeApp.Services;

public class JokeDAO : IJokeDataService
{
    private readonly IConfiguration _configuration;

    public JokeDAO(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Delete(Joke joke)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
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
    public List<Joke> GetAllJokes()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
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
    public List<Joke> SearchJokes(string search)
    {

        List<Joke> jokes = new List<Joke>();
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
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

    public int Insert(Joke joke)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
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

    public int Update(Joke joke)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
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

    public Joke FindId(int? id)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
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

    //Function that returns a joke at random
    public Joke RandomJoke()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        Joke? joke = null;

        string sqlStatement = "SELECT TOP 1 * FROM dbo.Joke ORDER BY NEWID()";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(sqlStatement, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    joke = new Joke
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
        return joke;
    }
}
    