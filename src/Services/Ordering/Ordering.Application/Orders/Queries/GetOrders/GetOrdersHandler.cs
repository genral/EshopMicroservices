

using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extentions;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.pageIndex;
            var pageSize = query.PaginationRequest.pageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders=await dbContext.Orders.Include(o=>o.orderItems)
                .OrderBy(o=>o.OrderName.Value)
                .Skip(pageIndex* pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>(
                pageIndex: pageIndex,
                pageSize: pageSize,
                count: totalCount,
                data: orders.ToOrderDtoList()
                ));    
        }
    }
}
