using Application.UseCases.Payment.GetPaymentStatus;
using Domain.Entity;
using Domain.Enum;
using Domain.Repository;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCasesTests;

public class GetPaymentStatusTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaymentStatus_WhenPaymentExists()
    {
        // Arrange
        var mockPaymentRepository = new Mock<IPaymentRepository>();
        var cancellationToken = CancellationToken.None;
        var payment = new PaymentDomain(Guid.NewGuid(), 100m, PaymentMethodEnum.Credito, PaymentStatusEnum.Aprovado);
        mockPaymentRepository.Setup(repo => repo.GetByOrderIdAsync(payment.OrderId, cancellationToken))
            .ReturnsAsync(payment);
        var useCase = new GetPaymentStatus(mockPaymentRepository.Object);
        var request = new GetPaymentStatusInput(payment.OrderId);

        // Act
        var result = await useCase.Handle(request, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(payment.Status, result.Value.Status);
        mockPaymentRepository.Verify(repo => repo.GetByOrderIdAsync(payment.OrderId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailResult_WhenPaymentNotFound()
    {
        // Arrange
        var mockPaymentRepository = new Mock<IPaymentRepository>();
        var cancellationToken = CancellationToken.None;
        mockPaymentRepository.Setup(repo => repo.GetByOrderIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync((PaymentDomain)null);
        var useCase = new GetPaymentStatus(mockPaymentRepository.Object);
        var request = new GetPaymentStatusInput(Guid.NewGuid());

        // Act
        var result = await useCase.Handle(request, cancellationToken);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Contains("not found", result.Errors[0].Message);
        mockPaymentRepository.Verify(repo => repo.GetByOrderIdAsync(It.IsAny<Guid>(), cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailResult_WhenExceptionIsThrown()
    {
        // Arrange
        var mockPaymentRepository = new Mock<IPaymentRepository>();
        var cancellationToken = CancellationToken.None;
        mockPaymentRepository.Setup(repo => repo.GetByOrderIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ThrowsAsync(new Exception("Database error"));
        var useCase = new GetPaymentStatus(mockPaymentRepository.Object);
        var request = new GetPaymentStatusInput(Guid.NewGuid());

        // Act
        var result = await useCase.Handle(request, cancellationToken);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Database error", result.Errors[0].Message);
        mockPaymentRepository.Verify(repo => repo.GetByOrderIdAsync(It.IsAny<Guid>(), cancellationToken), Times.Once);
    }
}
