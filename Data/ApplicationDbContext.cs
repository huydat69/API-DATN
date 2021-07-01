using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public virtual DbSet<tblKeHoachGiangDay> KeHoachGiangDays { get; set; }
        public virtual DbSet<tblCanBoGiangVien> tblCanBoGiangViens { get; set; }
        public virtual DbSet<tblChiTietPhieuThu> tblChiTietPhieuThus { get; set; }
        public virtual DbSet<tblDiemHocPhan> tblDiemHocPhans { get; set; }
        public virtual DbSet<tblDiemHocPhanChiTiet> tblDiemHocPhanChiTiets { get; set; }
        public virtual DbSet<tblDinhMucHocPhi> tblDinhMucHocPhis { get; set; }
        public virtual DbSet<tblHocPhan> tblHocPhans { get; set; }
        public virtual DbSet<tblKhoanThu> tblKhoanThus { get; set; }
        public virtual DbSet<tblKhoanThuSinhVien> tblKhoanThuSinhViens { get; set; }
        public virtual DbSet<tblLophoc> tblLophocs { get; set; }
        public virtual DbSet<tblNganhDaoTao> tblNganhDaoTaos { get; set; }
        public virtual DbSet<tblPhieuThu> tblPhieuThus { get; set; }
        public virtual DbSet<tblPhongKhoa> tblPhongKhoas { get; set; }
        public virtual DbSet<tblSinhvien> tblSinhviens { get; set; }
        public virtual DbSet<tblSinhVienLopHoc> tblSinhVienLopHocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
        }
    }
}
