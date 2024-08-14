namespace ProductCatalog.Core.Exceptions
{
    public class ForbiddenCoreException : RestCoreException
    {
        public ForbiddenCoreException() : base()
        {
        }

        public ForbiddenCoreException(string? message, Exception? inner) : base(message, inner)
        {
        }
    }
}
