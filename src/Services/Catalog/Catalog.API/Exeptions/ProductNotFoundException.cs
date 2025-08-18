namespace Catalog.API.Exeptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException():base("Product not Found")
        {
        }
    }
}
