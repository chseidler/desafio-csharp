namespace Application.UseCases.Order.CreateOrder;

public class CreateOrder : ICreateOrder
{
    public Task<CreateOrderOutput> Handle(CreateOrderInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
