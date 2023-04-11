using HistoryService.Features.Application.Dto;

namespace HistoryService.Features.Application.Repositories
{
    public interface IRepository
    {
        IEnumerable<QueryResultDtoHistory> GetAll();
    }
}