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
    public async Task<IActionResult> MakePayment(MakePaymentInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);
        if (result.IsSuccess)
            return CreatedAtAction(nameof(MakePayment), new { id = result.Value.PaymentId }, result.Value);

        return BadRequest(result.Errors.Select(e => e.Message).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPaymentStatus(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPaymentStatusInput(id), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.Select(e => e.Message).ToList());
    }

    [HttpPost("{id:guid}")]
    public async Task<IActionResult> RequestRefund(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RequestRefundInput(id), cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.Select(e => e.Message).ToList());
    }
}