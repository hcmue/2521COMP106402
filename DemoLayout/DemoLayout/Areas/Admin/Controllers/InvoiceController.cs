using Microsoft.AspNetCore.Mvc;

namespace DemoLayout.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        // /Admin/Invoice/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
