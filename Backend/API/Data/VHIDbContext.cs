using API.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace API.Data
{
    public class VHIDbContext : DbContext
    {
        public VHIDbContext(DbContextOptions<VHIDbContext> options) : base(options)
        {
        }
        public DbSet<BenhVien> BenhVien { get; set; }
        public DbSet<GoiBaoHiem> GoiBaoHiem { get; set; }
        
        public DbSet<ChinhSach> ChinhSach { get; set; }
        public DbSet<BenhVien_GoiBaoHiem> BenhVien_GoiBaoHiem { get; set; }
        public DbSet<ChiTietChinhSach> ChiTietChinhSach { get; set; }
        public DbSet<QuanLyBaoHiem> QuanLyBaoHiem { get; set; }
        public DbSet<YeuCauChiTra> YeuCauChiTra { get; set; }
        public DbSet<LichSuChiTra> LichSuChiTra { get; set; }
        public DbSet<PhieuThanhToanBaoHiem> PhieuThanhToanBaoHiem { get; set; }
    }
}
