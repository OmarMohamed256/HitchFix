using Microsoft.AspNetCore.Mvc;

namespace HitchFrontEnd.Controllers
{
    public class DashBoardController : Controller
    {
        [Route("admin")]
        public IActionResult DashBoardIndex()
        {
            return View();
        }
    }
}
