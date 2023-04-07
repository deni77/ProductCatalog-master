using MediatR;

namespace ProductCatalog.Application.Abstraction.Messaging
{
   public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
