using ApplicationException = ProductCatalog.Domain.Exception.ApplicationException;
namespace ProductCatalog.Application.Exceptions
{
    public sealed class ValidationException : Domain.Exception.ApplicationException
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred")
            => ErrorsDictionary = errorsDictionary;

        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }
    }
}
