namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductCommandValidatior : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidatior()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is Required");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is Required")
                .Length(2, 50).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

        }
    }
}
