using System.Data;
using Dapper;
using ScheduleMicroservice.Domain.Entities.Models;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;

namespace ScheduleMicroservice.Infrastructure.Repository;

public class AppointmentsRepository : IAppointmentsRepository
{
    private readonly DapperContext _db;
    private const string UpdateAppointmentStatusProcedure = "UpdateAppointmentStatus";
    private const string CreateAppointmentProcedure = "CreateAppointment";
    private const string DeleteAppointmentProcedure = "DeleteAppointment";
    private const string GetAppointmentsWithResultProcedure = "GetAppointmentsWithResult";
    private const string GetAppointmentsAsDoctorProcedure = "GetAppointmentsAsDoctor";
    private const string GetAppointmentsAsPatientProcedure = "GetAppointmentsAsPatient";
    private const string GetAppointmentsProcedure = "GetAppointments";
    private const string GetAppointmentByIdProcedure = "GetAppointmentById";
    private const string UpdateAppointmentProcedure = "UpdateAppointment";
    private const string UpdateAppointmentServiceNameProcedure = "UpdateAppointmentServiceName";
    private const string UpdateAppointmentDoctorNameProcedure = "UpdateAppointmentDoctorName";
    private const string UpdateAppointmentPatientNameProcedure = "UpdateAppointmentPatientName";

    public AppointmentsRepository(DapperContext db)
    {
        _db = db;
    }

    public async Task ChangeStatusAsync(Guid id, bool status)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Status", status, DbType.Boolean, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(UpdateAppointmentStatusProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<Appointment> CreateAsync(Appointment model)
    {
        var id = Guid.NewGuid();
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("PatientId",model.PatientId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("DoctorId", model.DoctorId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("ServiceId", model.ServiceId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Date", model.Date, DbType.Date, ParameterDirection.Input);
        parameters.Add("Time", model.Time, DbType.Time, ParameterDirection.Input);
        parameters.Add("ServiceName", model.ServiceName, DbType.String, ParameterDirection.Input);
        parameters.Add("DoctorFirstName", model.DoctorFirstName, DbType.String, ParameterDirection.Input);
        parameters.Add("DoctorLastName", model.DoctorLastName, DbType.String, ParameterDirection.Input);
        parameters.Add("DoctorMiddleName", model.DoctorMiddleName, DbType.String, ParameterDirection.Input);
        parameters.Add("PatientFirstName", model.PatientFirstName, DbType.String, ParameterDirection.Input);
        parameters.Add("PatientLastName", model.PatientLastName, DbType.String, ParameterDirection.Input);
        parameters.Add("PatientMiddleName", model.PatientMiddleName, DbType.String, ParameterDirection.Input);
        using (var connection = _db.CreateConnection())
        {
            await connection.ExecuteAsync(CreateAppointmentProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        return await GetByIdAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(DeleteAppointmentProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<Appointment> GetAppointmentWithResultAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

        using var connection = _db.CreateConnection();
        using var multi =
            await connection.QueryMultipleAsync(GetAppointmentsWithResultProcedure, parameters, commandType: CommandType.StoredProcedure);
        var appointmentWithResult = await multi.ReadSingleOrDefaultAsync<Appointment>();
        if (appointmentWithResult != null)
            appointmentWithResult.Result = await multi.ReadSingleOrDefaultAsync<Result>();
        return appointmentWithResult;
    }

    public async Task<List<Appointment>> GetAsDoctorAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        var company = await connection.QueryAsync<Appointment>
            (GetAppointmentsAsDoctorProcedure, parameters, commandType: CommandType.StoredProcedure);
        return company.ToList();
    }

    public async Task<List<Appointment>> GetAsPatientAsync(Guid id)
    {
        var parameters = new DynamicParameters();

        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        var appointments = await connection.QueryAsync<Appointment>
            (GetAppointmentsAsPatientProcedure, parameters, commandType: CommandType.StoredProcedure);
        return appointments.ToList();
    }

    public async Task<List<Appointment>> GetAsync()
    {
        var parameters = new DynamicParameters();
        using var connection = _db.CreateConnection();
        var appointments = await connection.QueryAsync<Appointment>
            (GetAppointmentsProcedure, parameters, commandType: CommandType.StoredProcedure);
        return appointments.ToList();
    }

    public async Task<Appointment> GetByIdAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        var appointment = await connection.QueryFirstOrDefaultAsync<Appointment>
            (GetAppointmentByIdProcedure, parameters, commandType: CommandType.StoredProcedure);
        return appointment;
    }

    public async Task UpdateAsync(Guid id, Appointment model)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("PatientId", model.PatientId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("DoctorId", model.DoctorId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("ServiceId", model.ServiceId, DbType.Guid, ParameterDirection.Input);
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
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(UpdateAppointmentProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateServiceNameAsync(Guid serviceId, string serviceName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("ServiceId", serviceId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("ServiceName", serviceName, DbType.String, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(UpdateAppointmentServiceNameProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateDoctorNameAsync(Guid doctorId, string doctorFirstName, string doctorLastName, string doctorMiddleName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("DoctorId", doctorId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("DoctorFirstName", doctorFirstName, DbType.String, ParameterDirection.Input);
        parameters.Add("DoctorLastName", doctorLastName, DbType.String, ParameterDirection.Input);
        parameters.Add("DoctorMiddleName", doctorMiddleName, DbType.String, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(UpdateAppointmentDoctorNameProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdatePatientNameAsync(Guid patientId, string patientFirstName, string patientLastName, string patientMiddleName)
    {
        var parameters = new DynamicParameters();
        parameters.Add("PatientId", patientId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("PatientFirstName", patientFirstName, DbType.String, ParameterDirection.Input);
        parameters.Add("PatientLastName", patientLastName, DbType.String, ParameterDirection.Input);
        parameters.Add("PatientMiddleName", patientMiddleName, DbType.String, ParameterDirection.Input);
        using var connection = _db.CreateConnection();
        await connection.ExecuteAsync(UpdateAppointmentPatientNameProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}