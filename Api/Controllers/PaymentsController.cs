using Api.ApiModels.Response;
using Application.UseCases.Payment.GetPaymentStatus;
using Application.UseCases.Payment.MakePayment;
using Application.UseCases.Payment.RequestRefund;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<MakePaymentOutput>), StatusCodes.Status201Created)]
    public async Task<IActionResult> MakePayment(MakePaymentInput input, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(nameof(MakePayment), new { id = output.PaymentId }, new ApiResponse<MakePaymentOutput>(output));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<GetPaymentStatusOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentStatus(Guid id, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetPaymentStatusInput(id), cancellationToken);
        return Ok(new ApiResponse<GetPaymentStatusOutput>(output));
    }

    [HttpPost("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<RequestRefundOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RequestRefund(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RequestRefundInput(id), cancellationToken);
        return NoContent();
    }
}