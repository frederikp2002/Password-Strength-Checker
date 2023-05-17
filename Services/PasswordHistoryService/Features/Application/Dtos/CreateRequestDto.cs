namespace PasswordHistoryService.Features.Application.Dtos;

public class CreateRequestDto
{
    public string PasswordHash { get; set; } = null!;
    public string PasswordStrength { get; set; } = null!;
    public DateTime PasswordDateTime { get; set; }
}