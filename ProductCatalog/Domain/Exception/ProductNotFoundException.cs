namespace ProductCatalog.Domain.Exception
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int Id)
            : base($"The product with the id {Id} was not found.")
        {
        }
    }
}
