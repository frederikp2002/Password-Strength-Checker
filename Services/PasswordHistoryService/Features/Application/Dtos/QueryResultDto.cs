namespace PasswordHistoryService.Features.Application.Dtos;

public class QueryResultDto
{
    public int PasswordId { get; set; }
    public string PasswordHash { get; set; } = null!;
    public string PasswordStrength { get; set; } = null!;
    public DateTime PasswordDateTime { get; set; }
}