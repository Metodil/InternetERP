namespace InternetERP.Web.ErrorHandlingMiddleware.Exceptions
{
    using System;

    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string message)
            : base(message)
        {
        }
    }
}
