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

    public async Task UpdateAsync(Guid id, ResultForUpdateDto model)
    {
        await _repository.UpdateAsync(id, _mapper.Map<Result>(model));
    }
}