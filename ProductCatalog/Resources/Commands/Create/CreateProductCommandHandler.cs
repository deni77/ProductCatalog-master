using MediatR;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Resources.Commands.Create
{
    //public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand> 
    //{
    //private readonly ProductDbContext _dbContext;

    //public CreateProductCommandHandler(ProductDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    //public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    //{
    //    //var product = new Product
    //    //{
    //    //    Name = request.Name,
    //    //    Description = request.Description,
    //    //    Category = request.Category,
    //    //    Price = request.Price,
    //    //};

    //    //_dbContext.Products.Add(product);
    //    //await _dbContext.SaveChangesAsync();
    //    //return product;
    //}

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ProductDbContext _dbContext;
         private readonly FakeDataStore _fakeDataStore;

        public CreateProductCommandHandler(ProductDbContext dbContext, FakeDataStore fakeDataStore)
        {
            _dbContext = dbContext;
            _fakeDataStore = fakeDataStore; 
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //dobavih za6toto update gyrme6e
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == request.Product.Id);

            if (product is null)
            {
                _dbContext.Products.Add(request.Product);
               //_fakeDataStore.AddProduct(request.Product); + ottapvame i Publish w AddProduct
            }
            else
            {
                _dbContext.Products.Update(request.Product);
            }
            //--dobavih


            await _dbContext.SaveChangesAsync();

            return request.Product;
        }

       
    }
}
