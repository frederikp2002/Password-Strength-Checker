using PasswordHistoryService.Features.Application.Dtos;
using PasswordHistoryService.Features.Application.Repositories;
using PasswordHistoryService.Features.Domain.Models;

namespace PasswordHistoryService.Features.Application.Commands.Implementations;

public class CreateCommand : ICreateCommand<CreateRequestDto>
{
    private readonly iRepository _repository;

    public CreateCommand(iRepository repository)
    {
        _repository = repository;
    }

    void ICreateCommand<CreateRequestDto>.Create(CreateRequestDto createRequestDto)
    {
        var entity = new PasswordEntity
        {
            PasswordHash = createRequestDto.PasswordHash,
            PasswordStrength = createRequestDto.PasswordStrength,
            PasswordDateTime = createRequestDto.PasswordDateTime
        };
        _repository.Create(entity);
    }
}