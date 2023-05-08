using Microsoft.EntityFrameworkCore;
using PasswordHistoryService.Features.Domain.Models;

namespace PasswordHistoryService.Data
{
    public class PasswordHistoryContext : DbContext
    {
        public PasswordHistoryContext(DbContextOptions<PasswordHistoryContext> options) : base(options)
        {
        }

        public DbSet<PasswordEntity> Passwords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PasswordHistory;Integrated Security=True");
        }
    }
}