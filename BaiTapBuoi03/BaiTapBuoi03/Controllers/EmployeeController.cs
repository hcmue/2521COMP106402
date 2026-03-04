using BaiTapBuoi03.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapBuoi03.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Còn lỗi trong quá trìnhđăng ký thông tin.");
                return View(employee);
            }
            return View();
        }

        public IActionResult CheckEmployeeNoExists(string EmployeeNo)
        {
            // Giả sử có một danh sách nhân viên đã tồn tại
            var existingEmployeeNos = new List<string> { "EMP001", "EMP002", "EMP003" };
            if (existingEmployeeNos.Contains(EmployeeNo))
            {
                return Json($"EmployeeNo '{EmployeeNo}' đã bị lấy.");
            }
            return Json(true);
        }
    }
}
