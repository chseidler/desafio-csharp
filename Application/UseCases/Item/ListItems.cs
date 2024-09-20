namespace Application.UseCases.Item;

public class ListItems : IListItems
{
    public Task<IReadOnlyList<ListItemsOutput>> Handle(ListItemsInput input, CancellationToken cancellationToken)
    {
        var orders = new List<ListItemsOutput>
        {
            new ListItemsOutput(new Guid(), 10m, 0),
            new ListItemsOutput(new Guid(), 10m, 0),
            new ListItemsOutput(new Guid(), 10m, 0),
        };

        return Task.FromResult<IReadOnlyList<ListItemsOutput>>(orders.AsReadOnly());
    }
}
