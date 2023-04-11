namespace HistoryService.Features.Application.Queries
{
    public interface IGetAllQuery<T>
    {
        IEnumerable<T> GetAll();
    }
}