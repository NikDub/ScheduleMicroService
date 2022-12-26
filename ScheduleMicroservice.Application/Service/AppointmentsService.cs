using AutoMapper;
using ScheduleMicroservice.Application.DTO.Appointments;
using ScheduleMicroservice.Application.Repository.Abstractions;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Application.Service
{
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

        public async Task<AppointmentsDTO> CreateAsync(AppointmentsForCreatedDTO model)
        {

            if (model == null)
                return null;

            var result = await _appointmentsRepository.CreateAsync(_mapper.Map<Appointment>(model));
            return _mapper.Map<AppointmentsDTO>(result);
        }

        public async Task DeleteAsync(string id)
        {
            await _appointmentsRepository.DeleteAsync(id);
        }

        public async Task<AppointmentsWithResultDTO> GetAppointmentWithResultAsync(string id)
        {
            var appontment = await _appointmentsRepository.GetAppintmentWithResult(id);
            if (appontment == null)
                return null;
            return _mapper.Map<AppointmentsWithResultDTO>(appontment);
        }

        public async Task<List<AppointmentsDTO>> GetAsDoctorAsync(string id)
        {
            return _mapper.Map<List<AppointmentsDTO>>(await _appointmentsRepository.GetAsPatientAsync(id));
        }

        public async Task<List<AppointmentsDTO>> GetAsPatientAsync(string id)
        {
            return _mapper.Map<List<AppointmentsDTO>>(await _appointmentsRepository.GetAsDoctorAsync(id));
        }

        public async Task<List<AppointmentsDTO>> GetAsync()
        {
            return _mapper.Map<List<AppointmentsDTO>>(await _appointmentsRepository.GetAsync());
        }

        public async Task<AppointmentsDTO> GetByIdAsync(string id)
        {
            var result = await _appointmentsRepository.GetByIdAsync(id);
            if (result == null)
                return null;
            return _mapper.Map<AppointmentsDTO>(result);
        }

        public async Task RescheduleAppointmentAsync(string id, AppointmentsForRescheduleDTO model)
        {
            var appointment = await _appointmentsRepository.GetByIdAsync(id);
            
            _mapper.Map(model, appointment);

            await _appointmentsRepository.UpdateAsync(id, appointment);
        }

        public async Task UpdateAsync(string id, AppointmentsForUpdateDTO model)
        {
            await _appointmentsRepository.UpdateAsync(id, _mapper.Map<Appointment>(model));
        }
    }
}
