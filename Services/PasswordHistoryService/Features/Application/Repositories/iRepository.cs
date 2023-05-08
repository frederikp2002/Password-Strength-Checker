using PasswordHistoryService.Features.Applications.Dtos;

namespace PasswordHistoryService.Features.Application.Repositories
{
    public interface iRepository
    {
        IEnumerable<QueryResultDto> GetAll();
    }
}