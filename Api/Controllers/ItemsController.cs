using Api.ApiModels.Response;
using Application.UseCases.Item;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ListItemsOutput>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListItems(CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new ListItemsInput(), cancellationToken);

        return Ok(new ApiResponse<IReadOnlyList<ListItemsOutput>>(output));
    }
}