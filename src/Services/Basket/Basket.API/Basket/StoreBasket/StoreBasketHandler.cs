using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBaskeCommandtHandler (IBasketRepository basketRepository)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {

            var cart=command.Cart;

            await basketRepository.StoreBasket(cart,cancellationToken);
             
            return new StoreBasketResult(cart.UserName);
        }
    }
}
