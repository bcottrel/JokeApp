using JokeApp.Models;

namespace JokeApp.Services;

public interface IJokeDataService
{
    List<Joke> GetAllJokes();
    List<Joke> SearchJokes(string search);
    Joke FindId(int? id);
    int Insert(Joke joke);
    int Update(Joke joke);
    void Delete(Joke joke);
    Joke RandomJoke();
}
