namespace PasswordHistoryService.Features.Application.Commands;

public interface ICreateCommand<T>
{
    void Create(T dto);
}