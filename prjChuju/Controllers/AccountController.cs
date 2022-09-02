using Microsoft.AspNetCore.Mvc;

namespace prjChuju.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
