using PasswordHistoryService.Features.Application.Dtos;
using PasswordHistoryService.Features.Domain.Models;

namespace PasswordHistoryService.Features.Application.Repositories;

public interface iRepository
{
    void Create(PasswordEntity passwordEntity);
    IEnumerable<QueryResultDto> GetAll();
}