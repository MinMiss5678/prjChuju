using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjChuju.Areas.Admin.Controllers
{
    [Authorize(Roles = "1")]
    public abstract class TestController1 : ControllerBase
    {
        public virtual IActionResult Index()
        {
            return Ok();
        }
    }
}
