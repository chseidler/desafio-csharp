using Application.UseCases.Order.CancelOrder;
using Application.UseCases.Order.CreateOrder;
using Application.UseCases.Order.GetOrderStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);

        if (result.IsSuccess)
            return CreatedAtAction(nameof(CreateOrder), new { id = result.Value.OrderId }, result.Value);

        return BadRequest(result.Errors.Select(e => e.Message).ToList());
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> CancelOrder(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CancelOrderInput(id), cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return BadRequest(result.Errors.Select(e => e.Message).ToList());

    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderStatus(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrderStatusInput(id), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.Select(e => e.Message).ToList());
    }
}