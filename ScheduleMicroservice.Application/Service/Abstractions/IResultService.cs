using ScheduleMicroservice.Application.DTO.Result;

namespace ScheduleMicroservice.Application.Service.Abstractions
{
    public interface IResultService
    {
        Task<List<ResultDTO>> GetAsync();
        Task<ResultDTO> GetByIdAsync(string id);
        Task<ResultDTO> CreateAsync(ResultForCreatedDTO model);
        Task UpdateAsync(string id, ResultForUpdateDTO model);
        Task DeleteAsync(string id);
    }
}
