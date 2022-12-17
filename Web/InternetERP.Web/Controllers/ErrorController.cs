using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetERP.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("error/404")]
        public IActionResult PageNotFound()
        {
            this.Response.Clear();
            this.Response.StatusCode = StatusCodes.Status404NotFound;
            return this.View("error-404");
        }

        [Route("error/500")]
        public IActionResult InternalServerError()
        {
            this.Response.Clear();
            this.Response.StatusCode = StatusCodes.Status404NotFound;
            return this.View("error-500");
        }

        [Route("error/{code}")]
        public IActionResult Index(int code)
        {
            this.Response.Clear();
            this.Response.StatusCode = code;
            return this.View("_GenericError");
        }
    }
}
