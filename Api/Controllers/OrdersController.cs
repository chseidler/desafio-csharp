using Api.ApiModels.Response;
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
    [ProducesResponseType(typeof(ApiResponse<CreateOrderOutput>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOrder(CreateOrderInput input, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(nameof(CreateOrder), new { id = output.OrderId }, new ApiResponse<CreateOrderOutput>(output));
    }

    [HttpPost("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelOrder(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CancelOrderInput(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetOrderStatusOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderStatus(Guid id, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetOrderStatusInput(id), cancellationToken);
        return Ok(new ApiResponse<GetOrderStatusOutput>(output));
    }
}