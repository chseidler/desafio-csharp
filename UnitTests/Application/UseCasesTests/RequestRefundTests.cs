using Application.Interfaces;
using Application.UseCases.Payment.RequestRefund;
using Domain.Entity;
using Domain.Enum;
using Domain.Repository;
using MediatR;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCasesTests;

public class RequestRefundTests
{
    [Fact]
    public async Task Handle_ShouldRefundOrder_WhenOrderExistsAndIsRefundable()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var item1 = new ItemDomain(Guid.NewGuid(), "Item 1", 100m, 10);
        var item2 = new ItemDomain(Guid.NewGuid(), "Item 2", 100m, 10);
        var order = new OrderDomain(Guid.NewGuid());
        order.Create(
        [
            (item1, 1),
            (item2, 1)
        ]);
        order.CompletePayment(200m);
        var payment = new PaymentDomain(orderId, 100, PaymentMethodEnum.Credito, PaymentStatusEnum.Aprovado);
        var mediatorMock = new Mock<IMediator>();
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var paymentRepositoryMock = new Mock<IPaymentRepository>();
        var paymentGatewayMock = new Mock<IPaymentGateway>();

        orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        paymentRepositoryMock.Setup(repo => repo.GetByOrderIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(payment);
        var requestRefund = new RequestRefund(orderRepositoryMock.Object, paymentRepositoryMock.Object, mediatorMock.Object, paymentGatewayMock.Object);
        var request = new RequestRefundInput(orderId);

        // Act
        var result = await requestRefund.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        orderRepositoryMock.Verify(repo => repo.UpdateAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        paymentRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<PaymentDomain>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenOrderDoesNotExist()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var paymentRepositoryMock = new Mock<IPaymentRepository>();
        var mediatorMock = new Mock<IMediator>();
        var paymentGatewayMock = new Mock<IPaymentGateway>();
        orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((OrderDomain)null);
        var requestRefund = new RequestRefund(orderRepositoryMock.Object, paymentRepositoryMock.Object, mediatorMock.Object, paymentGatewayMock.Object);
        var request = new RequestRefundInput(orderId);

        // Act
        var result = await requestRefund.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Order with ID {orderId} not found.", result.Errors.First().Message);
    }
}
