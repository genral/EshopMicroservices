 
namespace Catalog.API.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category field is Required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile field is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
