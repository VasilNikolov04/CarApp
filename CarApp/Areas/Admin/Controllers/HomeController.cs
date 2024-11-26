using Microsoft.AspNetCore.Mvc;

namespace CarApp.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
