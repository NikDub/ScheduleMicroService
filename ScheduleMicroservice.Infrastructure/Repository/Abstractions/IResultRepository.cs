using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Infrastructure.Repository.Abstractions;

public interface IResultRepository
{
    Task<Result> GetByIdAsync(Guid id);
    Task<Result> CreateAsync(Result model);
    Task UpdateAsync(Guid id, Result model);
}