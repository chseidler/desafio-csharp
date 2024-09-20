using Domain.Repository;

namespace Application.UseCases.Item;

public class ListItems : IListItems
{
    private readonly IItemRepository _repository;

    public ListItems(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListItemsOutput>> Handle(ListItemsInput input, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAsync(cancellationToken);

        var output = items.Select(item => new ListItemsOutput(
            item.Id,
            item.Name,
            item.Price,
            item.QuantityInStock
        )).ToList();

        return output;
    }
}
