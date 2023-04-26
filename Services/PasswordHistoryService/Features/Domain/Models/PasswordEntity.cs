using System.ComponentModel.DataAnnotations;

namespace PasswordHistoryService.Features.Domain.Models
{
    public class PasswordEntity
    {
        [Key] public int PasswordId { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime PasswordDateTime { get; set; }
    }
}