namespace PasswordHistoryService.Features.Applications.Queries
{
    public interface IGetAllQuery<T>
    {
        IEnumerable<T> GetAll();
    }
}