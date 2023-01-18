using System.Data;
using Dapper;
using ScheduleMicroservice.Domain.Entities.Models;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;

namespace ScheduleMicroservice.Infrastructure.Repository;

public class ResultRepository : IResultRepository
{
    private readonly DapperContext _db;
    private const string CreateResultProcedure = "CreateResult";
    private const string GetResultByIdProcedure = "GetResultById";
    private const string UpdateResultProcedure = "UpdateResult";

    public ResultRepository(DapperContext db)
    {
        _db = db;
    }

    public async Task<Result> CreateAsync(Result model)
    {
        var id = Guid.NewGuid();

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("AppointmentId", model.AppointmentId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Complaints", model.Complaints, DbType.String, ParameterDirection.Input);
        parameters.Add("Conclusion", model.Conclusion, DbType.String, ParameterDirection.Input);
        parameters.Add("Recommendations", model.Recommendations, DbType.String, ParameterDirection.Input);

        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(CreateResultProcedure, parameters, commandType: CommandType.StoredProcedure);

        return await GetByIdAsync(id);
    }

    private async Task<Result> GetByIdAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<Result>
            (GetResultByIdProcedure, parameters, commandType: CommandType.StoredProcedure);
        return result;
    }

    public async Task UpdateAsync(Guid id, Result model)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("AppointmentId", model.AppointmentId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Complaints", model.Complaints, DbType.String, ParameterDirection.Input);
        parameters.Add("Conclusion", model.Conclusion, DbType.String, ParameterDirection.Input);
        parameters.Add("Recommendations", model.Recommendations, DbType.String, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(UpdateResultProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}