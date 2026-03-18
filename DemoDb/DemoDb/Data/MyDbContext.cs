using Microsoft.EntityFrameworkCore;

namespace DemoDb.Data
{
    public class MyDbContext : DbContext
    {
        //1. Viết hàm tạo
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        //2. Khai báo thuộc tính ứng với Entity Model (bảng)
        public DbSet<Loai> Loais { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
    }
}
