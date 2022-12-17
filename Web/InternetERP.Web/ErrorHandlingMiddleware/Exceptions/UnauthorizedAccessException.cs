namespace InternetERP.Web.ErrorHandlingMiddleware.Exceptions
{
    using System;

    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message)
            : base(message)
        {
        }
    }
}
