using Microsoft.AspNetCore.Mvc;

namespace Buoi02.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult ABC()
        {
            return Redirect("/Home/Privacy");
        }

        public IActionResult XYZ()
        {
            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult MyAPI()
        {
            return Json(new { Time = DateTime.Now, FullName = "Admin"});
        }
    }
}
