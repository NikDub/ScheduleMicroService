using AutoMapper;
using ScheduleMicroservice.Application.DTO.Appointments;
using ScheduleMicroservice.Application.DTO.Result;
using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroService.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Result, ResultDTO>().ReverseMap();
            CreateMap<Result, ResultForCreatedDTO>().ReverseMap();
            CreateMap<Result, ResultForUpdateDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentsDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentsForCreatedDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentsForUpdateDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentsWithResultDTO>()
                .ForMember("ResultId", r=> r.MapFrom(c=>c.Result.Id))
                .ForMember("Complaints", r => r.MapFrom(c => c.Result.Complaints))
                .ForMember("Conclusion", r => r.MapFrom(c => c.Result.Conclusion))
                .ForMember("Recommendations", r => r.MapFrom(c => c.Result.Recommendations));

            CreateMap<AppointmentsForRescheduleDTO, Appointment>().ReverseMap();
        }
    }
}
