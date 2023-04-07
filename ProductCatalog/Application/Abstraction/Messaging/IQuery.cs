using MediatR;

namespace ProductCatalog.Application.Abstraction.Messaging
{
     public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
