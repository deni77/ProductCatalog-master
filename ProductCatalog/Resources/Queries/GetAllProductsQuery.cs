using MediatR;
using ProductCatalog.Models;

namespace ProductCatalog.Resources.Queries
{
   public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>
{
}
}
