
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket
{
    public class DeletBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeletBasketCommandValidator()
        {
                RuleFor(x=>x.UserName).NotEmpty().WithMessage("username is required");
        }
    }
}
