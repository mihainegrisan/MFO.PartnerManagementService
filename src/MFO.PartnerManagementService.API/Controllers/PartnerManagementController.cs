using FluentResults;
using MediatR;
using MFO.PartnerManagementService.Application.Features.Queries;
using MFO.PartnerManagementService.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace MFO.PartnerManagementService.API.Controllers;

[ApiController]
[Route("api/partners")]
public class PartnerManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartnerManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Synchronous API used by the Listing Service for real-time validation. (QUERY)
    /// </summary>
    [HttpGet("{partnerId}")]
    public async Task<IActionResult> GetPartnerDetailsForValidation([FromRoute] Guid partnerId)
    {
        var query = new GetPartnerDetailsForValidationQuery(partnerId);

        var result = await _mediator.Send(query);

        if (result.IsFailed)
        {
            // 2. Centralized Error Handling: Map the FluentResult error to an HTTP response.
            // This logic can (and should) be extracted into a common base class or filter/middleware.
            return MapResultToActionResult(result);
        }

        // Return the DTO from the handler
        return Ok(result);
    }

    /// <summary>
    /// Simple utility to map FluentResult to IActionResult.
    /// In production, this would be a robust base controller method or action filter.
    /// </summary>
    private IActionResult MapResultToActionResult<T>(Result<T> result)
    {
        // Check for specific errors added in the handler
        if (result.HasError<NotFoundError>())
        {
            // You can log the error here if needed
            return NotFound(new { errors = result.Errors.Select(e => e.Message) });
        }

        // Default failure for unhandled errors
        return StatusCode(500, new { errors = result.Errors.Select(e => e.Message) });
    }
}