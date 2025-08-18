using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):IQuery<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    internal class DeleteProductCommandHandler (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
        : IQueryHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);


            var product = await session.LoadAsync<Product>(command.Id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            session.Delete(product);
            await session.SaveChangesAsync();

            return new DeleteProductResult(IsSuccess: true);
        }
    }
}
