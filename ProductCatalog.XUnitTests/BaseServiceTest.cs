using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Data;
using ProductCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.XUnitTests
{
    public abstract class BaseServiceTests : IDisposable
    {
        protected IConfigurationRoot Configuration { get; set; }
        protected IServiceProvider ServiceProvider { get; set; }

        protected ProductDbContext DbContext { get; set; }

       protected BaseServiceTests()
        {
             this.Configuration = this.SetConfiguration();
            var services = this.SetServices();
            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ProductDbContext>();
            this.Seed();
        }


        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ProductDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            return services;

        }
        private IConfigurationRoot SetConfiguration()
        {
            return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettings.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();
        }

        private void Seed()
        {
            var product = new Product
            {
                Name = "testCountry",
                Description="dddddd", Category="cat", IsActive="true", Price=10.20M
            };

            this.DbContext.Products.Add(product);
            DbContext.SaveChanges();    
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
