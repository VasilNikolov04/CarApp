using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CarApp.Infrastructure.Constants.ApplicationConstants;

namespace CarApp.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminBaseController : Controller
    {

    }
}
