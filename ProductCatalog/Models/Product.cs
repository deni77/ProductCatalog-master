using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public  class Product //sealed
    {
       public Product(string name, string description)
             : this()
        {
            Name = name;
            Description = description;
        }

        public   Product()
        {
        }

        public int Id { get; private set; }

        public string Name { get;  set; }

        public string Description { get;  set; }

        public string Category { get;  set; }

        public string IsActive { get;  set; }

        public decimal Price { get;  set; }

        public void Update(string name, string description) => (Name, Description) = (name, description);

    }
}
