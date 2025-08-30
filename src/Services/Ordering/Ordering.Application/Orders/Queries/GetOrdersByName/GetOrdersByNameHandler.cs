 
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data; 
using Ordering.Application.Extentions; 

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler (IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders= await dbContext.Orders.Include(o=>o.orderItems).AsNoTracking()
                .Where(o=>o.OrderName.Value.Contains(query.Name))
                .OrderBy(o=>o.OrderName.Value)
                .ToListAsync(cancellationToken);
              
            return new GetOrderByNameResult(orders.ToOrderDtoList());
        } 
    }
}
