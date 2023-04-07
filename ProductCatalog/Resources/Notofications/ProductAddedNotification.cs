using MediatR;
using ProductCatalog.Models;

namespace ProductCatalog.Resources.Notofications
{
    public record ProductAddedNotification(Product Product) : INotification;
}
