
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.EndPoints
{
    //public record GetOrdersByCustomerRequest(Guid Id);
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);

    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var results = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                var response= results.Adapt<GetOrderByCustomerResponse> ();

                return Results.Ok(response);

            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrderByCustomerResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
        }
    }
}
