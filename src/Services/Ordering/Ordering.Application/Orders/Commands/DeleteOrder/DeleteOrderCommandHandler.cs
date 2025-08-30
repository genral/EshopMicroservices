using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResponse>
    {
        public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId=OrderId.Of(command.OrderId);

            var order=await dbContext.Orders.FindAsync(orderId);
            if (order == null) {
                throw new OrderNotFoundException(command.OrderId);
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResponse(true);
        }
    }
}
