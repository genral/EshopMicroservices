using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):IQuery<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);


    internal class DeleteProductCommandHandler (IDocumentSession session )
        : IQueryHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {   
            var product = await session.LoadAsync<Product>(command.Id);

            if (product == null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            session.Delete(product);
            await session.SaveChangesAsync();

            return new DeleteProductResult(IsSuccess: true);
        }
    }
}
