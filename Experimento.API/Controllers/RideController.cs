using Experimento.Application.UseCases;
using Experimento.Application.UseCases.CreateRide;
using Experimento.Application.UseCases.CreateRideByUserId;
using Experimento.Application.UseCases.DeleteRideById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Experimento.Controllers;

[Route("api/Ride")]
[ApiController]
public class RideController : ControllerBase
{
    private readonly IMediator _mediator;

    public RideController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRideCommand dados, CancellationToken cancellationToken)
    {
        await _mediator.Send(dados, cancellationToken);

        return NoContent();
    }
    
    [HttpGet("rider/{riderId}")]
    public async Task<IActionResult> GetRidesByRiderId(string riderId, CancellationToken cancellationToken)
    {
        var query = new ListRidesByRiderIdQuery { RiderId = riderId };
        var rides = await _mediator.Send(query, cancellationToken);

        return Ok(rides);
    }
    
    [HttpDelete("{rideId}")]
    public async Task<IActionResult> DeleteRide(string rideId, CancellationToken cancellationToken)
    {
        var query = new DeleteRideByIdCommand { RideId = rideId };
        await _mediator.Send(query, cancellationToken);
        
        return NoContent();
    }
}