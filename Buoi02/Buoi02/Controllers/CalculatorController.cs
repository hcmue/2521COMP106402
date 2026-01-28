using Microsoft.AspNetCore.Mvc;

namespace Buoi02.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculate(double SoHang01, double SoHang02, string ToanTu)
        {
            double KetQua = 0;
            switch(ToanTu)
            {
                case "^": KetQua = Math.Pow(SoHang01, SoHang02); break;
                case "%": KetQua = SoHang01 % SoHang02; break;
                case "*": KetQua = SoHang01 * SoHang02; break;
            }


            ViewBag.So01 = SoHang01;
            ViewBag.So02 = SoHang02;
            ViewBag.ToanTu = ToanTu;
            ViewBag.KetQua = KetQua;
            return View("Index");
        }
    }
}
