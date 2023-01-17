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

    public async Task ApproveStatusAsync(Guid id)
    {
        await _appointmentsRepository.ChangeStatusAsync(id, true);
    }

    public async Task<AppointmentsDto> CreateAsync(AppointmentForCreatedDto model)
    {
        if (model == null)
            return null;

        var result = await _appointmentsRepository.CreateAsync(_mapper.Map<Appointment>(model));
        return _mapper.Map<AppointmentsDto>(result);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _appointmentsRepository.DeleteAsync(id);
    }

    public async Task<AppointmentsWithResultDto> GetAppointmentWithResultAsync(Guid id)
    {
        var appointment = await _appointmentsRepository.GetAppointmentWithResult(id);
        if (appointment == null)
            return null;
        return _mapper.Map<AppointmentsWithResultDto>(appointment);
    }

    public async Task<List<AppointmentsDto>> GetAsDoctorAsync(Guid id)
    {
        return _mapper.Map<List<AppointmentsDto>>(await _appointmentsRepository.GetAsDoctorAsync(id));
    }

    public async Task<List<AppointmentsDto>> GetAsPatientAsync(Guid id)
    {
        return _mapper.Map<List<AppointmentsDto>>(await _appointmentsRepository.GetAsPatientAsync(id));
    }

    public async Task<List<AppointmentsDto>> GetAsync()
    {
        return _mapper.Map<List<AppointmentsDto>>(await _appointmentsRepository.GetAsync());
    }

    public async Task<AppointmentsDto> GetByIdAsync(Guid id)
    {
        var result = await _appointmentsRepository.GetByIdAsync(id);
        if (result == null)
            return null;
        return _mapper.Map<AppointmentsDto>(result);
    }

    public async Task RescheduleAppointmentAsync(Guid id, AppointmentsForRescheduleDto model)
    {
        await _appointmentsRepository.UpdateAsync(id, _mapper.Map<Appointment>(model));
    }

    public async Task UpdateAsync(Guid id, AppointmentsForUpdateDto model)
    {
        await _appointmentsRepository.UpdateAsync(id, _mapper.Map<Appointment>(model));
    }
}