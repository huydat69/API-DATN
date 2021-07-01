using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Data.Repositories
{
    public class ClientRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;

        public object TraCuuDiem(string maSV, string maLop, string maHP)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand sqlComm = new SqlCommand("ClientTraCuuDiem", conn);
                sqlComm.Parameters.AddWithValue("@p_MaSV", maSV);
                sqlComm.Parameters.AddWithValue("@p_MaLop", maLop);
                sqlComm.Parameters.AddWithValue("@p_MaHP", maHP);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(data);
            }
            List<Dto.Tracuudiem> sv = new List<Dto.Tracuudiem>();
            sv = (from DataRow dr in data.Tables[0].Rows
                  select new Dto.Tracuudiem()
                  {
                      MaSV = dr["MaSV"].ToString(),
                      HoVaTen = dr["HoVaTen"].ToString(),
                      NgaySinh = string.IsNullOrEmpty(dr["NgaySinh"].ToString())
                        ? DateTime.Now : DateTime.Parse(dr["NgaySinh"].ToString()),
                      TenLop = dr["HoVaTen"].ToString(),
                      NgayVaoLop = string.IsNullOrEmpty(dr["NgayVaoLop"].ToString())
                        ? DateTime.Now : DateTime.Parse(dr["NgayVaoLop"].ToString()),
                      TenNganh = dr["TenNganh"].ToString(),
                      TenPhongKhoa = dr["TenPhongKhoa"].ToString(),
                      HocKy = string.IsNullOrEmpty(dr["HocKy"].ToString())
                        ? 0 : Convert.ToInt32(dr["HocKy"]),
                      NamHoc = string.IsNullOrEmpty(dr["NamHoc"].ToString())
                        ? 0 : Convert.ToInt32(dr["NamHoc"]),
                      Diem = string.IsNullOrEmpty(dr["Diem"].ToString())
                        ? 0 : Convert.ToDouble(dr["Diem"]),
                      MaHP = dr["MaHP"].ToString(),
                      TenHocPhan = dr["TenHocPhan"].ToString(),
                      SoTinChi = string.IsNullOrEmpty(dr["SoTinChi"].ToString())
                        ? 0 : Convert.ToDouble(dr["SoTinChi"]),
                      HeSo = string.IsNullOrEmpty(dr["HeSo"].ToString())
                        ? 0 : Convert.ToDouble(dr["HeSo"]),
                      DiemThanhPhan = dr["DiemThanhPhan"].ToString(),
                      SoLanHoc = string.IsNullOrEmpty(dr["SoLanHoc"].ToString())
                        ? 0 : Convert.ToInt32(dr["SoLanHoc"]),
                  }).ToList();

            if (!sv.Any()) return null;

            return sv.FirstOrDefault();
        }

        public object TraCuuKhoanThu(string maKhoanThu)
        {
            var result = from t1 in DbContext.tblKhoanThus
                         where t1.MaKhoanThu.Trim() == maKhoanThu.Trim()
                         select new
                         {
                             t1.MaKhoanThu,
                             t1.MoTa,
                         };

            if (!result.Any())
                return null;
            return result.SingleOrDefault();
        }

        public object KhoanThuSinhVien(string maSv)
        {
            var result = from t1 in DbContext.tblKhoanThuSinhViens
                         join t2 in DbContext.tblKhoanThus on t1.MaKhoanThu equals t2.MaKhoanThu into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblPhieuThus on t1.MaSV equals t3.NguoiNop into x2
                         from t3 in x2.DefaultIfEmpty()
                         where t1.MaSV == maSv
                         select new
                         {
                             t1.MaKhoanThu,
                             t2.MoTa,
                             t2.HocKy,
                             t2.NamHocTu,
                             t2.NamHocDen,
                             t2.SoTien,
                             SoTienPhaiNop = DbContext.tblChiTietPhieuThus.FirstOrDefault(x => x.MaKhoanThu == t1.MaKhoanThu).SoTienThu,
                             SoTienDaNop = DbContext.tblChiTietPhieuThus.FirstOrDefault(x => x.MaKhoanThu == t1.MaKhoanThu).SoTienNop,
                             SoTienThuaThieu = DbContext.tblChiTietPhieuThus.FirstOrDefault(x => x.MaKhoanThu == t1.MaKhoanThu).SoTienThuaThieu
                         };
            if (!result.Any())
                return null;
            return result.ToList();
        }

        public IEnumerable<tblKhoanThuSinhVien> IEKhoanThuSinhVien(string maSv)
        {
            var result = DbContext.tblKhoanThuSinhViens.Where(x => x.MaSV == maSv).ToList();
            return result;
        }

        public tblKhoanThu Khoanthu_Detail(string maKhoanThu)
        {
            var result = DbContext.tblKhoanThus.FirstOrDefault(x => x.MaKhoanThu == maKhoanThu);
            return result;
        }

        public async Task<tblKhoanThuSinhVien> TaoKhoanThuSinhVien(tblKhoanThuSinhVien tbl)
        {
            if (DbContext.tblKhoanThuSinhViens.Any(x => x.MaSV == tbl.MaSV && x.MaKhoanThu == tbl.MaKhoanThu))
                return null;
            DbContext.tblKhoanThuSinhViens.Add(tbl);
            await DbContext.SaveChangesAsync();
            return tbl;
        }

        public IEnumerable<tblSinhVienLopHoc> SinhVienLopHoc(string Malop)
        {
            var result = DbContext.tblSinhVienLopHocs.Where(x => x.MaLop == Malop).ToList();
            return result;
        }

        public async Task<tblPhieuThu> TaoPhieuThu(tblPhieuThu tbl)
        {
            DbContext.tblPhieuThus.Add(tbl);
            await DbContext.SaveChangesAsync();
            return tbl;
        }

        public async Task<tblChiTietPhieuThu> TaoChiTietPhieuThu(tblChiTietPhieuThu tbl)
        {
            DbContext.tblChiTietPhieuThus.Add(tbl);
            await DbContext.SaveChangesAsync();
            return tbl;
        }

        public bool Check_CTPhieuthu(string masv, string makhoanthu)
        {
            var result = (from t1 in DbContext.tblPhieuThus
                          join t2 in DbContext.tblChiTietPhieuThus on t1.MaPhieuThu equals t2.MaPhieuThu
                          where t1.NguoiNop == masv && t2.MaKhoanThu == makhoanthu
                          select t2).Any();

            if (result) return false;

            return true;
        }
    }
}
