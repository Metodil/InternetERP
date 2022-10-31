namespace InternetERP.Web.Areas.Employee.Controllers
{
    using InternetERP.Common;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.EmployeeRoleName)]
    [Area("Employee")]
    public class EmployeeController : Controller
    {
    }
}
