using AutoMapper;
using ScheduleMicroservice.Application.DTO.Appointments;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Domain.Entities.Models;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;

namespace ScheduleMicroservice.Application.Service;

public class AppointmentsService : IAppointmentsService
{
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IMapper _mapper;

    public AppointmentsService(IAppointmentsRepository appointmentsRepository, IMapper mapper)
    {
        _appointmentsRepository = appointmentsRepository;
        _mapper = mapper;
    }

    public async Task ChangeStatusAsync(string id, bool status = true)
    {
        await _appointmentsRepository.ChangeStatusAsync(id, status);
    }

    public async Task<AppointmentsDto> CreateAsync(AppointmentsForCreatedDto model)
    {
        if (model == null)
            return null;

        var result = await _appointmentsRepository.CreateAsync(_mapper.Map<Appointment>(model));
        return _mapper.Map<AppointmentsDto>(result);
    }

    public async Task DeleteAsync(string id)
    {
        await _appointmentsRepository.DeleteAsync(id);
    }

    public async Task<AppointmentsWithResultDto> GetAppointmentWithResultAsync(string id)
    {
        var appontment = await _appointmentsRepository.GetAppintmentWithResult(id);
        if (appontment == null)
            return null;
        return _mapper.Map<AppointmentsWithResultDto>(appontment);
    }

    public async Task<List<AppointmentsDto>> GetAsDoctorAsync(string id)
    {
        return _mapper.Map<List<AppointmentsDto>>(await _appointmentsRepository.GetAsDoctorAsync(id));
    }

    public async Task<List<AppointmentsDto>> GetAsPatientAsync(string id)
    {
        return _mapper.Map<List<AppointmentsDto>>(await _appointmentsRepository.GetAsPatientAsync(id));
    }

    public async Task<List<AppointmentsDto>> GetAsync()
    {
        return _mapper.Map<List<AppointmentsDto>>(await _appointmentsRepository.GetAsync());
    }

    public async Task<AppointmentsDto> GetByIdAsync(string id)
    {
        var result = await _appointmentsRepository.GetByIdAsync(id);
        if (result == null)
            return null;
        return _mapper.Map<AppointmentsDto>(result);
    }

    public async Task RescheduleAppointmentAsync(string id, AppointmentsForRescheduleDto model)
    {
        var appointment = await _appointmentsRepository.GetByIdAsync(id);

        _mapper.Map(model, appointment);

        await _appointmentsRepository.UpdateAsync(id, appointment);
    }

    public async Task UpdateAsync(string id, AppointmentsForUpdateDto model)
    {
        await _appointmentsRepository.UpdateAsync(id, _mapper.Map<Appointment>(model));
    }
}