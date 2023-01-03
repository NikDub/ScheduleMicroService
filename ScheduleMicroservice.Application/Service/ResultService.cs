using AutoMapper;
using ScheduleMicroservice.Application.DTO.Result;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Domain.Entities.Models;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;

namespace ScheduleMicroservice.Application.Service;

public class ResultService : IResultService
{
    private readonly IMapper _mapper;
    private readonly IResultRepository _repository;

    public ResultService(IResultRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultDto> CreateAsync(ResultForCreatedDto model)
    {
        if (model == null)
            return null;

        var result = await _repository.CreateAsync(_mapper.Map<Result>(model));
        return _mapper.Map<ResultDto>(result);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<List<ResultDto>> GetAsync()
    {
        return _mapper.Map<List<ResultDto>>(await _repository.GetAsync());
    }

    public async Task<ResultDto> GetByIdAsync(string id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null)
            return null;
        return _mapper.Map<ResultDto>(result);
    }

    public async Task UpdateAsync(string id, ResultForUpdateDto model)
    {
        await _repository.UpdateAsync(id, _mapper.Map<Result>(model));
    }
}