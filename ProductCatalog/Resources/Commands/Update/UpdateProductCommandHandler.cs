using MediatR;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Resources.Commands.Update
{
     public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly ProductDbContext _dbContext;

        public UpdateProductCommandHandler(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //samo s warianta bez kontroller se polzwa
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == request.Id);

            if (product is null)
                return default;

                
            product.Update(request.Name, request.Description);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return product;
        }
    }
}
