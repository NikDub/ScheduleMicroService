﻿using ScheduleMicroservice.Application.DTO.Result;

namespace ScheduleMicroservice.Application.Service.Abstractions;

public interface IResultService
{
    Task<ResultDto> CreateAsync(ResultForCreatedDto model);
    Task UpdateAsync(string id, ResultForUpdateDto model);
}