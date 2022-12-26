using AutoMapper;
using ScheduleMicroservice.Application.DTO.Result;
using ScheduleMicroservice.Application.Repository.Abstractions;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Application.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repository;
        private readonly IMapper _mapper;
        public ResultService(IResultRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResultDTO> CreateAsync(ResultForCreatedDTO model)
        {
            if (model == null)
                return null;

            var result = await _repository.CreateAsync(_mapper.Map<Result>(model));
            return _mapper.Map<ResultDTO>(result);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<ResultDTO>> GetAsync()
        {
            return _mapper.Map<List<ResultDTO>>(await _repository.GetAsync());
        }

        public async Task<ResultDTO> GetByIdAsync(string id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return null;
            return _mapper.Map<ResultDTO>(result);
        }

        public async Task UpdateAsync(string id, ResultForUpdateDTO model)
        {
            await _repository.UpdateAsync(id, _mapper.Map<Result>(model));
        }
    }
}
