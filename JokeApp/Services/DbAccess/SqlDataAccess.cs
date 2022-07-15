using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace JokeApp.Services.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _configuration;

    public SqlDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U param,
        string connectionId = "DefaultConnection")
    {
        using IDbConnection connection = new SqlConnection(
            _configuration.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure, param,
            commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
        string storedProcedure,
        T param,
        string connectionId = "DefaultConnection")
    {
        using IDbConnection connection = new SqlConnection(
            _configuration.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, param,
            commandType: CommandType.StoredProcedure);
    }
}