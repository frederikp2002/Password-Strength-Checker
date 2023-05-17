using PasswordHistoryService.Data;
using PasswordHistoryService.Features.Application.Dtos;
using PasswordHistoryService.Features.Application.Repositories;
using PasswordHistoryService.Features.Domain.Models;

namespace PasswordHistoryService.Features.Infrastructure.Repositories.Implementations;

public class Repository : iRepository
{
    private readonly PasswordHistoryContext _db;

    public Repository(PasswordHistoryContext db)
    {
        _db = db;
    }

    public void Create(PasswordEntity passwordEntity)
    {
        _db.Add(passwordEntity);
        _db.SaveChanges();
    }

    IEnumerable<QueryResultDto> iRepository.GetAll()
    {
        foreach (var dbEntity in _db.Passwords)
            yield return new QueryResultDto
            {
                PasswordId = dbEntity.PasswordId,
                PasswordHash = dbEntity.PasswordHash,
                PasswordStrength = dbEntity.PasswordStrength,
                PasswordDateTime = dbEntity.PasswordDateTime
            };
    }
}