using Dapper;
using ScheduleMicroservice.Application.Repository.Abstractions;
using ScheduleMicroservice.Domain.Entities.Models;
using ScheduleMicroservice.Infrastructure;
using System.Data;

namespace ScheduleMicroservice.Application.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly DapperContext _db;
        public ResultRepository(DapperContext db)
        {
            _db = db;
        }

        public async Task<Result> CreateAsync(Result model)
        {
            var id = Guid.NewGuid();
            var procedureName = "CreateResult";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Guid, ParameterDirection.Input);
            parameters.Add("AppointmentsId", Guid.Parse(model.AppointmentsId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("Complaints", model.Complaints, DbType.String, ParameterDirection.Input);
            parameters.Add("Conclusion", model.Conclusion, DbType.String, ParameterDirection.Input);
            parameters.Add("Recommendations", model.Recommendations, DbType.String, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return await GetByIdAsync(id.ToString());
        }

        public async Task DeleteAsync(string id)
        {
            var procedureName = "DeleteResult";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Result>> GetAsync()
        {
            var procedureName = "GetResult";
            var parameters = new DynamicParameters();
            using (var connection = _db.CreateConnection())
            {
                var appointments = await connection.QueryAsync<Result>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return appointments.ToList();
            }
        }

        public async Task<Result> GetByIdAsync(string id)
        {
            var procedureName = "GetResultById";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Result>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task UpdateAsync(string id, Result model)
        {
            var procedureName = "UpdateResult";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            parameters.Add("AppointmentsId", Guid.Parse(model.AppointmentsId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("Complaints", model.Complaints, DbType.String, ParameterDirection.Input);
            parameters.Add("Conclusion", model.Conclusion, DbType.String, ParameterDirection.Input);
            parameters.Add("Recommendations", model.Recommendations, DbType.String, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
