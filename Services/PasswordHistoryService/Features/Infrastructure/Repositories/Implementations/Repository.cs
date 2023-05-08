using PasswordHistoryService.Data;
using PasswordHistoryService.Features.Application.Repositories;
using PasswordHistoryService.Features.Applications.Dtos;

namespace PasswordHistoryService.Features.Infrastructure.Repositories.Implementations
{
    public class Repository : iRepository
    {
        private readonly PasswordHistoryContext _db;

        public Repository(PasswordHistoryContext db)
        {
            _db = db;
        }

        IEnumerable<QueryResultDto> iRepository.GetAll()
        {
            foreach (var dbEntity in _db.Passwords)
            {
                yield return new QueryResultDto
                {
                    PasswordId = dbEntity.PasswordId,
                    PasswordHash = dbEntity.PasswordHash,
                    PasswordStrength = dbEntity.PasswordStrength,
                    PasswordDateTime = dbEntity.PasswordDateTime
                };
            }
        }
    }
}