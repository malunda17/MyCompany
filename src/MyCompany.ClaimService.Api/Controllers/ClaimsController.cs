using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.ClaimService.Application.Commands;
using MyCompany.ClaimService.Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.ClaimService.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClaimsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [HttpGet("{claimId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClaimDTO>> Get([FromRoute] int claimId)
        {
            var claim = await _mediator.Send(new GetClaimQuery(claimId));
            if (claim == null)
            {
                return NotFound();
            }

            return claim;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ClaimDTO>> Get()
        {
            return await _mediator.Send(new GetAllClaimsQuery());
        }

        /// <summary>
        /// Create a Claim
        /// </summary>
        /// <remarks>
        /// Sample request
        ///
        ///     POST /Claims
        ///     {
        ///
        ///        "userId": 100,
        ///        "date": "2020-12-30T20:36:22.472Z",
        ///        "description": "lorem",
        ///        "incidence": "lorem",
        ///        "damagedItem": "lorem",
        ///        "street": "Imara",
        ///        "city": "Lubumbashi",
        ///        "country": "DRC"
        ///      }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>A newly created claim</returns>
        /// <response code="201">Returns the newly created claim </response>
        /// <response code="400">If the claim is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateClaimCommand command)
        {
            return CreatedAtAction(nameof(Get), new { claimId = await _mediator.Send(command) });
        }

        /// <summary>
        /// Delete a specific Claim
        /// </summary>
        /// <param name="claimId"></param>
        /// <returns></returns>
        [HttpDelete("{claimId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int claimId)
        {
            var result = await _mediator.Send(new DeleteClaimCommand(claimId));
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{claimId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateClaimCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}