using JokeApp.Model;
using JokeApp.Services.DbAccess;

namespace JokeApp.Services.Data;

public class JokeData : IJokeData
{
    private readonly ISqlDataAccess _db;

    public JokeData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<Joke>> GetJokes() =>
        _db.LoadData<Joke, dynamic>(
        storedProcedure: "dbo.spJoke_GetAll", new { });

    public async Task<Joke?> GetJoke(int id)
    {
        var results = await _db.LoadData<Joke, dynamic>(
        storedProcedure: "dbo.spJoke_Get",
        new { Id = id });

        return results.FirstOrDefault();
    }

    public Task InsertJoke(Joke joke) =>
        _db.SaveData(storedProcedure: "dbo.spJoke_Insert",
        new { joke.JokeQuestion, joke.JokeAnswer });

    public Task UpdateJoke(Joke joke) =>
        _db.SaveData(storedProcedure: "dbo.spJoke_Update",
        joke);

    public Task DeleteJoke(int id) =>
        _db.SaveData(storedProcedure: "dbo.spJoke_Delete",
        new { Id = id });
}
