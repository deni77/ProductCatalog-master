using MediatR;
using ProductCatalog.Application.Abstraction.Messaging;
using ProductCatalog.Data;
using ProductCatalog.Domain.Exception;

namespace ProductCatalog.Application.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Unit>
    {
       private readonly ProductDbContext _dbContext;
        
        public UpdateProductCommandHandler(ProductDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           var product = _dbContext.Products.FirstOrDefault(p => p.Id == request.Id);

            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            product.Name = request.Name;
             product.Description = request.Description;

          // product.Update(request.Name, request.Description);
             _dbContext.Update(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
