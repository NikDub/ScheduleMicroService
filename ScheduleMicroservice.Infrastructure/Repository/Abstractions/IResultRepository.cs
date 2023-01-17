using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Infrastructure.Repository.Abstractions;

public interface IResultRepository
{
    Task<List<Result>> GetAsync();
    Task<Result> GetByIdAsync(Guid id);
    Task<Result> CreateAsync(Result model);
    Task UpdateAsync(Guid id, Result model);
    Task DeleteAsync(Guid id);
}