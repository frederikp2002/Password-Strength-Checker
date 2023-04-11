using HistoryService.Features.Application.Dto;
using HistoryService.Features.Application.Repositories;

namespace HistoryService.Features.Infrastructure.Repositories.Implementations
{
    public class Repository
    {
        IEnumerable<QueryResultDtoHistory> IRepository.GetAll()
        {
            foreach (var dbPasswordEntity COLLECTION)
            {
                yield return new QueryResultDtoHistory()
                {
                }
            }
        }
    }
}
