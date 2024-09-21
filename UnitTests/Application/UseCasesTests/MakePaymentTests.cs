using Application.Interfaces;
using Application.UseCases.Payment.MakePayment;
using Domain.Entity;
using Domain.Enum;
using Domain.Repository;
using MediatR;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCasesTests;

public class MakePaymentTests
{
    [Fact]
    public async Task Handle_ShouldReturnError_WhenOrderNotFound()
    {
        // Arrange
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockPaymentRepository = new Mock<IPaymentRepository>();
        var mockMediator = new Mock<IMediator>();
        var mockPaymentGateway = new Mock<IPaymentGateway>();
        var makePayment = new MakePayment(mockOrderRepository.Object, mockPaymentRepository.Object, mockMediator.Object, mockPaymentGateway.Object);
        var request = new MakePaymentInput(Guid.NewGuid(), PaymentMethodEnum.Credito);
        mockOrderRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((OrderDomain)null);

        // Act
        var result = await makePayment.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal($"Order with ID {request.OrderId} not found.", result.Errors.First().Message);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenOrderIsNotInCorrectState()
    {
        // Arrange
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockPaymentRepository = new Mock<IPaymentRepository>();
        var mockMediator = new Mock<IMediator>();
        var mockPaymentGateway = new Mock<IPaymentGateway>();
        var makePayment = new MakePayment(mockOrderRepository.Object, mockPaymentRepository.Object, mockMediator.Object, mockPaymentGateway.Object);
        var request = new MakePaymentInput(Guid.NewGuid(), PaymentMethodEnum.Credito);
        var item1 = new ItemDomain(Guid.NewGuid(), "Item 1", 100m, 10);
        var item2 = new ItemDomain(Guid.NewGuid(), "Item 2", 100m, 10);
        var order = new OrderDomain(Guid.NewGuid());
        order.Create(
        [
            (item1, 1),
            (item2, 1)
        ]);
        order.Cancel();
        mockOrderRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        // Act
        var result = await makePayment.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Order must be in 'Aguardando Processamento' state to process payment.", result.Errors.First().Message);
    }

    [Fact]
    public async Task Handle_ShouldProcessPaymentSuccessfully_WhenPaymentIsApproved()
    {
        // Arrange
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockPaymentRepository = new Mock<IPaymentRepository>();
        var mockMediator = new Mock<IMediator>();
        var mockPaymentGateway = new Mock<IPaymentGateway>();
        var makePayment = new MakePayment(mockOrderRepository.Object, mockPaymentRepository.Object, mockMediator.Object, mockPaymentGateway.Object);
        var request = new MakePaymentInput(Guid.NewGuid(), PaymentMethodEnum.Credito);
        var item1 = new ItemDomain(Guid.NewGuid(), "Item 1", 100m, 10);
        var item2 = new ItemDomain(Guid.NewGuid(), "Item 2", 100m, 10);
        var order = new OrderDomain(Guid.NewGuid());
        order.Create(
        [
            (item1, 1),
            (item2, 1)
        ]);
        mockOrderRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);
        mockPaymentGateway.Setup(gateway => gateway.ProcessPaymentAsync(It.IsAny<decimal>()))
            .ReturnsAsync(true); // Simula um pagamento bem-sucedido

        // Act
        var result = await makePayment.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(order.Id, result.Value.PaymentId);
        mockPaymentRepository.Verify(repo => repo.SaveAsync(It.IsAny<PaymentDomain>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
