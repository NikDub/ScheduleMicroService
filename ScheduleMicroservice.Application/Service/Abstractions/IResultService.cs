using ScheduleMicroservice.Application.DTO.Result;

namespace ScheduleMicroservice.Application.Service.Abstractions;

public interface IResultService
{
    Task<List<ResultDto>> GetAsync();
    Task<ResultDto> GetByIdAsync(string id);
    Task<ResultDto> CreateAsync(ResultForCreatedDto model);
    Task UpdateAsync(string id, ResultForUpdateDto model);
    Task DeleteAsync(string id);
}