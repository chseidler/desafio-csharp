using Application.UseCases.Item;
using Application.UseCases.Order.CreateOrder;
using Application.UseCases.Order.GetOrderStatus;
using Application.UseCases.Payment.GetPaymentStatus;
using Application.UseCases.Payment.MakePayment;
using Application.UseCases.Payment.RequestRefund;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Api.Configurations.Policies;

[JsonSerializable(typeof(IReadOnlyList<ListItemsOutput>))]
[JsonSerializable(typeof(List<ListItemsOutput>))]
[JsonSerializable(typeof(CreateOrderOutput))]
[JsonSerializable(typeof(GetOrderStatusOutput))]
[JsonSerializable(typeof(CreateOrderInput))]
[JsonSerializable(typeof(MakePaymentOutput))]
[JsonSerializable(typeof(MakePaymentInput))]
[JsonSerializable(typeof(GetPaymentStatusOutput))]
[JsonSerializable(typeof(RequestRefundOutput))]
[JsonSerializable(typeof(Guid))]
[JsonSerializable(typeof(List<String>))]
[JsonSerializable(typeof(IEnumerable<String>))]
[JsonSerializable(typeof(ValidationProblemDetails))]
public partial class MyJsonContext : JsonSerializerContext
{
}
