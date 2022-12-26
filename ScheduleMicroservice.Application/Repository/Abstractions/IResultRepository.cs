using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Application.Repository.Abstractions
{
    public interface IResultRepository
    {
        Task<List<Result>> GetAsync();
        Task<Result> GetByIdAsync(string id);
        Task<Result> CreateAsync(Result model);
        Task UpdateAsync(string id, Result model);
        Task DeleteAsync(string id);
    }
}
