using Microsoft.AspNetCore.Mvc;

namespace DemoLayout.Models
{
    public class CategoryMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //đọc database lấy Category
            var danhMuc = new List<Category>
            {
                new Category {Id=1, Name="Tủ lạnh" },
                new Category {Id=2, Name="Điều hòa" }
            };
            return View(danhMuc);
        }
    }
}
