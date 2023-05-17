namespace PasswordHistoryService.Features.Application.Queries;

public interface IGetAllQuery<T>
{
    IEnumerable<T> GetAll();
}