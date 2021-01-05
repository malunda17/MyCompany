using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.LogService.Application.Commands;
using MyCompany.LogService.Application.Models;
using MyCompany.LogService.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.LogService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class LogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

       
        [HttpDelete("{logId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int logId)
        {
            var result = await _mediator.Send(new DeleteLogCommand(logId));
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("{logId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LogViewModel>> Get([FromRoute] int logId)
        {
            var log = await _mediator.Send(new GetLogQuery(logId));
            if (log == null)
            {
                return NotFound();
            }

            return log;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<LogViewModel>> Get()
        {
            return await _mediator.Send(new GetAllLogsQuery());
        }
    }
}