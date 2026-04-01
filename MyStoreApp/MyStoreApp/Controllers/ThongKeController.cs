using Microsoft.AspNetCore.Mvc;
using MyStoreApp.Entities;

namespace MyStoreApp.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly MyeStoreContext _context;

        public ThongKeController(MyeStoreContext context)
        {
            _context = context;
        }

        public IActionResult ThongKeTheoLoai()
        {
            var data = _context.ChiTietHds
                .GroupBy(g => new
                {
                    g.MaHhNavigation.MaLoai,
                    g.MaHhNavigation.MaLoaiNavigation.TenLoai
                })
                .Select(g => new
                {
                    g.Key.MaLoai,
                    g.Key.TenLoai,
                    DoanhThu = g.Sum(ct => ct.SoLuong * ct.DonGia)
                }).ToList();
            return Json(data);
        }
    }
}
