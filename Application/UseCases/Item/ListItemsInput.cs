using MediatR;

namespace Application.UseCases.Item;

public class ListItemsInput : IRequest<IReadOnlyList<ListItemsOutput>>
{
}
