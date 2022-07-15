namespace JokeApp.Services.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U param, string connectionId = "DefaultConnection");
    Task SaveData<T>(string storedProcedure, T param, string connectionId = "DefaultConnection");
}