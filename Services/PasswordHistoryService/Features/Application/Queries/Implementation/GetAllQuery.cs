using PasswordHistoryService.Features.Application.Dtos;
using PasswordHistoryService.Features.Application.Queries;
using PasswordHistoryService.Features.Application.Repositories;

namespace PasswordHistoryService.Features.Applications.Queries.Implementation;

public class GetAllQuery : IGetAllQuery<QueryResultDto>
{
    private readonly iRepository _repository;

    public GetAllQuery(iRepository repository)
    {
        _repository = repository;
    }

    IEnumerable<QueryResultDto> IGetAllQuery<QueryResultDto>.GetAll()
    {
        return _repository.GetAll();
    }
}