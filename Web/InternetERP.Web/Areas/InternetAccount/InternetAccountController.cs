namespace InternetERP.Web.Areas.Employee.Controllers
{
    using InternetERP.Common;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.InternetAccountRoleName)]
    [Area("InternetAccount")]
    public class InternetAccountController : Controller
    {
    }
}
