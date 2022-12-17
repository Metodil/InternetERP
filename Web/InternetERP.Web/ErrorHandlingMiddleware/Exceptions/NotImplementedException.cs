namespace InternetERP.Web.ErrorHandlingMiddleware.Exceptions
{
    using System;

    public class NotImplementedException : Exception
    {
        public NotImplementedException(string message)
            : base(message)
        {
        }
    }
}
