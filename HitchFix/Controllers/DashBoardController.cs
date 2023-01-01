using Microsoft.AspNetCore.Mvc;

namespace HitchFix.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult DashBoardIndex()
        {
            return View();
        }
    }
}
