namespace ProductCatalog.Domain.Exception
{
    public abstract class ApplicationException :System.Exception
    {
        protected ApplicationException(string title, string message)
            : base(message) =>
            Title = title;

        public string Title { get; }
    }
}
