using Api.ApiModels.Response;
using Application.UseCases.Item;
using Application.UseCases.Order.CreateOrder;
using Application.UseCases.Order.GetOrderStatus;
using Application.UseCases.Payment.GetPaymentStatus;
using Application.UseCases.Payment.MakePayment;
using Application.UseCases.Payment.RequestRefund;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Api.Configurations.Policies;

[JsonSerializable(typeof(ApiResponse<IReadOnlyList<ListItemsOutput>>))]
[JsonSerializable(typeof(ApiResponse<CreateOrderOutput>))]
[JsonSerializable(typeof(ApiResponse<GetOrderStatusOutput>))]
[JsonSerializable(typeof(CreateOrderInput))]
[JsonSerializable(typeof(ApiResponse<MakePaymentOutput>))]
[JsonSerializable(typeof(ApiResponse<MakePaymentInput>))]
[JsonSerializable(typeof(ApiResponse<GetPaymentStatusOutput>))]
[JsonSerializable(typeof(ApiResponse<RequestRefoundOutput>))]
[JsonSerializable(typeof(Guid))]
[JsonSerializable(typeof(ProblemDetails))]
public partial class MyJsonContext : JsonSerializerContext
{
}
