 using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBaskeCommandtHandler (IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
             
            var cart=command.Cart;

            await DeductDiscount(cart, cancellationToken);

            await basketRepository.StoreBasket(cart,cancellationToken);
             
            return new StoreBasketResult(cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupen = await discountProtoServiceClient
                    .GetDiscountAsync(new GetDiscountRequest{ ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupen.Amount;
            }
        }
    }
}
