namespace Application.UseCases.Order.GetOrderStatus;

public class GetOrderStatus : IGetOrderStatus
{
    public Task<GetOrderStatusOutput> Handle(GetOrderStatusInput request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
