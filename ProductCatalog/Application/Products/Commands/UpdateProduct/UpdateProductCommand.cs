using MediatR;
using ProductCatalog.Application.Abstraction.Messaging;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(int Id, string Name, string Description) : ICommand<Unit>;
  
}
