using JokeApp.Model;

namespace JokeApp.Services
{
    public interface IJokeDataService
    {
        List<Joke> GetAllJokes(IConfiguration configuration);
        List<Joke> SearchJokes(string search, IConfiguration configuration);
        Joke FindId(int? id, IConfiguration configuration);
        int Insert(Joke joke, IConfiguration configuration);
        int Update(Joke joke, IConfiguration configuration);
        void Delete(Joke joke, IConfiguration configuration);
        Joke RandomJoke(IConfiguration configuration);
    }
}
