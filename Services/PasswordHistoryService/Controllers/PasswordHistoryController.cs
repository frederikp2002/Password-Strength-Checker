using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordHistoryService.Features.Applications.Dtos;
using PasswordHistoryService.Features.Applications.Queries;

namespace PasswordHistoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordHistoryController : ControllerBase
    {
        private readonly IGetAllQuery<QueryResultDto> _getAllQuery;

        public PasswordHistoryController(IGetAllQuery<QueryResultDto> getAllQuery)
        {
            _getAllQuery = getAllQuery;
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
                return NotFound("help");
            }
        }
    }
}