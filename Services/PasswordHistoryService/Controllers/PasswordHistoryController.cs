using Microsoft.AspNetCore.Mvc;
using PasswordHistoryService.Features.Application.Commands;
using PasswordHistoryService.Features.Application.Dtos;
using PasswordHistoryService.Features.Application.Queries;

namespace PasswordHistoryService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PasswordHistoryController : ControllerBase
{
    private readonly ICreateCommand<CreateRequestDto> _createCommand;
    private readonly IGetAllQuery<QueryResultDto> _getAllQuery;

    public PasswordHistoryController(
        ICreateCommand<CreateRequestDto> createCommand,
        IGetAllQuery<QueryResultDto> getAllQuery)
    {
        _createCommand = createCommand;
        _getAllQuery = getAllQuery;
    }

    [HttpPost("Create")]
    public ActionResult Post([FromBody] CreateRequestDto createRequestDto)
    {
        try
        {
            _createCommand.Create(createRequestDto);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<QueryResultDto>> GetAll()
    {
        try
        {
            var result = _getAllQuery.GetAll().ToList();
            return result;
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}