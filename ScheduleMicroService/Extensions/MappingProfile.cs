using AutoMapper;
using ScheduleMicroservice.Application.DTO.Appointments;
using ScheduleMicroservice.Application.DTO.Result;
using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroService.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Result, ResultDto>().ReverseMap();
        CreateMap<Result, ResultForCreatedDto>().ReverseMap();
        CreateMap<Result, ResultForUpdateDto>().ReverseMap();

        CreateMap<Appointment, AppointmentsDto>().ReverseMap();
        CreateMap<Appointment, AppointmentForCreatedDto>().ReverseMap();
        CreateMap<Appointment, AppointmentsForUpdateDto>().ReverseMap();

        CreateMap<Appointment, AppointmentsWithResultDto>()
            .ForMember(r=>r.ResultId, r => r.MapFrom(c => c.Result.Id))
            .ForMember(r => r.Complaints, r => r.MapFrom(c => c.Result.Complaints))
            .ForMember(r => r.Conclusion, r => r.MapFrom(c => c.Result.Conclusion))
            .ForMember(r => r.Recommendations, r => r.MapFrom(c => c.Result.Recommendations));

        CreateMap<AppointmentsForRescheduleDto, Appointment>().ReverseMap();
    }
}