﻿using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects; 

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order):ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidatior : AbstractValidator<OrderDto>
    {
        public UpdateOrderCommandValidatior()
        {
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer id is required");
            RuleFor(x => x.OrderName).NotEmpty().WithMessage("Order name required");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
        }
    }

    public class UpdateOrderCommandHandler (IApplicationDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var order= await dbContext.Orders.FindAsync(orderId, cancellationToken);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }
            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);

        }

        private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress= Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(orderName:OrderName.Of(orderDto.OrderName),shippingAddress:updatedBillingAddress ,billingAddress:updatedBillingAddress, payment:updatedPayment, status: orderDto.Status);

        }
    }


}
