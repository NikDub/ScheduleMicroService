using Dapper;
using ScheduleMicroservice.Application.Repository.Abstractions;
using ScheduleMicroservice.Domain.Entities.Models;
using ScheduleMicroservice.Infrastructure;
using System.Data;

namespace ScheduleMicroservice.Application.Repository
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly DapperContext _db;
        public AppointmentsRepository(DapperContext db)
        {
            _db = db;
        }

        public async Task ChangeStatusAsync(string id, bool status)
        {
            var procedureName = "UpdateAppointmentsStatus";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            parameters.Add("Status", status, DbType.Boolean, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Appointment> CreateAsync(Appointment model)
        {
            var id = Guid.NewGuid();
            var procedureName = "CreateAppointments";
            var parameters = new DynamicParameters();
            parameters.Add("ID", id, DbType.Guid, ParameterDirection.Input);
            parameters.Add("PatientId", Guid.Parse(model.PatientId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("DoctorId",  Guid.Parse(model.DoctorId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("ServiceId", Guid.Parse( model.ServiceId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("Date", model.Date, DbType.Date, ParameterDirection.Input);
            parameters.Add("Time", model.Time, DbType.Time, ParameterDirection.Input);
            parameters.Add("Status", model.Status, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("ServiceName", model.ServiceName, DbType.String, ParameterDirection.Input);
            parameters.Add("DoctorFirstName", model.DoctorFirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("DoctorLastName", model.DoctorLastName, DbType.String, ParameterDirection.Input);
            parameters.Add("DoctorMiddleName", model.DoctorMiddleName, DbType.String, ParameterDirection.Input);
            parameters.Add("PatientFirstName", model.PatientFirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("PatientLastName", model.PatientLastName, DbType.String, ParameterDirection.Input);
            parameters.Add("PatientMiddleName", model.PatientMiddleName, DbType.String, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }

            return await GetByIdAsync(id.ToString());
        }

        public async Task DeleteAsync(string id)
        {
            var procedureName = "DeleteAppointments";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Appointment> GetAppintmentWithResult(string id)
        {
            var procedureName = "GetAppointmentsWithResult";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);

            using (var connection = _db.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(procedureName, parameters, commandType: CommandType.StoredProcedure))
            {
                var company = await multi.ReadSingleOrDefaultAsync<Appointment>();
                if (company != null)
                    company.Result = await multi.ReadSingleOrDefaultAsync<Result>();
                return company;
            }
        }

        public async Task<List<Appointment>> GetAsDoctorAsync(string id)
        {
            var procedureName = "GetAppointmentsAsDoctor";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                var company = await connection.QueryAsync<Appointment>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company.ToList();
            }
        }

        public async Task<List<Appointment>> GetAsPatientAsync(string id)
        {
            var procedureName = "GetAppointmentsAsPatient";
            var parameters = new DynamicParameters();

            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                var company = await connection.QueryAsync<Appointment>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company.ToList();
            }
        }

        public async Task<List<Appointment>> GetAsync()
        {
            var procedureName = "GetAppointments";
            var parameters = new DynamicParameters();
            using (var connection = _db.CreateConnection())
            {
                var appointments = await connection.QueryAsync<Appointment>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return appointments.ToList();
            }
        }

        public async Task<Appointment> GetByIdAsync(string id)
        {
            var procedureName = "GetAppointmentsById";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Appointment>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task UpdateAsync(string id, Appointment model)
        {
            var procedureName = "UpdateAppointments";
            var parameters = new DynamicParameters();
            parameters.Add("ID", Guid.Parse(id), DbType.Guid, ParameterDirection.Input);
            parameters.Add("PatientId", Guid.Parse(model.PatientId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("DoctorId", Guid.Parse(model.DoctorId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("ServiceId", Guid.Parse(model.ServiceId), DbType.Guid, ParameterDirection.Input);
            parameters.Add("Date", model.Date, DbType.Date, ParameterDirection.Input);
            parameters.Add("Time", model.Time, DbType.Time, ParameterDirection.Input);
            parameters.Add("Status", model.Status, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("ServiceName", model.ServiceName, DbType.String, ParameterDirection.Input);
            parameters.Add("DoctorFirstName", model.DoctorFirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("DoctorLastName", model.DoctorLastName, DbType.String, ParameterDirection.Input);
            parameters.Add("DoctorMiddleName", model.DoctorMiddleName, DbType.String, ParameterDirection.Input);
            parameters.Add("PatientFirstName", model.PatientFirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("PatientLastName", model.PatientLastName, DbType.String, ParameterDirection.Input);
            parameters.Add("PatientMiddleName", model.PatientMiddleName, DbType.String, ParameterDirection.Input);
            using (var connection = _db.CreateConnection())
            {
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
