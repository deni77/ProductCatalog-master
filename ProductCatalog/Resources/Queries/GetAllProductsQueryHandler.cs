using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Resources.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly ProductDbContext _context;
        public GetAllProductsQueryHandler(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) =>
            await _context.Products.ToListAsync();

        //    private readonly FakeDataStore _fakeDataStore;
        //public GetAllProductsQueryHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
        //public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request,
        //    CancellationToken cancellationToken) => await _fakeDataStore.GetAllProducts();
        //}
    }
}
