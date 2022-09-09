using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjChuju.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public class HomeController : Controller
    {
        [Area(areaName: "Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
