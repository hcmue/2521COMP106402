using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStoreApp.Entities;
using MyStoreApp.Models;

namespace MyStoreApp.Controllers
{
    public class HangHoasController : Controller
    {
        private readonly MyeStoreContext _context;

        public HangHoasController(MyeStoreContext context)
        {
            _context = context;
        }

        #region Search HangHoa
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        const int SO_SP_MOT_TRANG = 15;
        [HttpPost]
        public IActionResult Search(string? Keyword, double? FromPrice, double? ToPrice, int page = 1)
        {
            var data = _context.HangHoas.AsQueryable();
            if (!string.IsNullOrEmpty(Keyword))
            {
                data = data.Where(p => p.TenHh.Contains(Keyword, StringComparison.OrdinalIgnoreCase));
            }
            if (FromPrice.HasValue)
            {
                data = data.Where(p => p.DonGia >= FromPrice.Value);
            }
            if (ToPrice.HasValue)
            {
                data = data.Where(p => p.DonGia <= ToPrice.Value);
            }
            var result = data.Select(p=> new HangHoaVM
            {
                MaHh = p.MaHh, TenHh = p.TenHh, Hinh = p.Hinh,
                DonGia = p.DonGia ?? 0,
                TenLoai = p.MaLoaiNavigation.TenLoai,
                NhaCungCap = p.MaNccNavigation.TenCongTy
            })
            .Skip((page -1)* SO_SP_MOT_TRANG)
            .Take(SO_SP_MOT_TRANG)
            .ToList();
            return View(result);
        }
        #endregion

        // GET: HangHoas
        public async Task<IActionResult> Index()
        {
            var myeStoreContext = _context.HangHoas.Include(h => h.MaLoaiNavigation).Include(h => h.MaNccNavigation);
            return View(await myeStoreContext.ToListAsync());
        }

        // GET: HangHoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: HangHoas/Create
        public IActionResult Create()
        {
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy");
            return View();
        }

        // POST: HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHh,TenHh,TenAlias,MaLoai,MoTaDonVi,DonGia,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                if (Hinh != null)
                {
                    hangHoa.Hinh = UploadFileToFolder(Hinh, "HangHoa");
                }
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        private string? UploadFileToFolder(IFormFile hinh, string folderName)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folderName, hinh.FileName);
            using (var f = new FileStream(fullPath, FileMode.Create))
            {
                hinh.CopyTo(f);
                return fullPath;
            }
        }

        // GET: HangHoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // POST: HangHoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHh,TenHh,TenAlias,MaLoai,MoTaDonVi,DonGia,Hinh,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa)
        {
            if (id != hangHoa.MaHh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loais, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHh == id);
        }
    }
}
