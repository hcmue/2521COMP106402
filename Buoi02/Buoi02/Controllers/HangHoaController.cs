using Buoi02.Models;
using Microsoft.AspNetCore.Mvc;

namespace Buoi02.Controllers
{
    public class HangHoaController : Controller
    {
        static List<HangHoa> hangHoas = new List<HangHoa>()
        {
            new HangHoa
            {
                MaHh = 1, TenHh = "IPHone 17 Pro", DonGia = 32000000, SoLuongTon = 13, Hinh = "ip17.jpg"
            },
            new HangHoa
            {
                MaHh = 2, TenHh = "IPHone 17 Pro", DonGia = 32000000, SoLuongTon = 13, Hinh = "ip17.jpg"
            }
        };

        public IActionResult Index()
        {
            return View(hangHoas);
        }

        #region Sửa hàng hóa
        public IActionResult Edit(int id)
        {
            var hangHoa = hangHoas.SingleOrDefault(p => p.MaHh == id);
            if (hangHoa != null)
            {
                return View(hangHoa);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(int id, HangHoa model)
        {
            // TODO
            return View();
        }
        #endregion

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HangHoa model, IFormFile UploadHinh)
        {
            var hangHoa = hangHoas.SingleOrDefault(p => p.MaHh == model.MaHh);
            if (UploadHinh != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "hanghoa", UploadHinh.FileName);
                using(var f = new FileStream(fullPath, FileMode.CreateNew))
                {
                    UploadHinh.CopyTo(f);
                    model.Hinh = UploadHinh.FileName;
                }
            }
            if (hangHoa == null)
            {
                hangHoas.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.ThongBaoLoi = $"Mã {model.MaHh} đã có";
            return View();
        }
    }
}
