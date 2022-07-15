using JokeApp.Model;

namespace DataAccess.Data;

public interface IJokeData
{
    Task DeleteJoke(int id);
    Task<Joke?> GetJoke(int id);
    Task<IEnumerable<Joke>> GetJokes();
    Task InsertJoke(Joke joke);
    Task UpdateJoke(Joke joke);
}