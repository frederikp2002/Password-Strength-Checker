using HistoryService.Features.Application.Dto;
using HistoryService.Features.Application.Queries;

namespace Services.HistoryService.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class HistoryController : Controller
    {
        private readonly IGetAllQuery<QueryResultDtoHistory> _getAllQuery;

        public HistoryController(IGetAllQuery<QueryResultDtoHistory> getAllQuery)
        {
            _getAllQuery = getAllQuery;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<QueryResultDtoHistory>> GetAll()
        {
            try
            {
                var result = _getAllQuery.GetAll().ToList();
                return result;
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}