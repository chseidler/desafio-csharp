using MediatR;

namespace Application.UseCases.Item;

public interface IListItems : IRequestHandler<ListItemsInput, IReadOnlyList<ListItemsOutput>>
{
}
