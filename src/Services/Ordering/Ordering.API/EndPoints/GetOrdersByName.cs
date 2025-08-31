
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.EndPoints
{
    //public record GetOrdersByNameRequest(string orderName);
    public record GetOrdersByNameReponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var results = await sender.Send(new GetOrdersByNameQuery(orderName));

                var response= results.Adapt<GetOrdersByNameReponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameReponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound) 
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
        }
    }
}
