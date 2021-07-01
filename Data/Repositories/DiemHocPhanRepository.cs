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
    public class DiemHocPhanRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
        public ApiResultPaging Search(PagingParam pagingParam, string maSv, int? hocky, int? namhoc, string maPK, string maLop, string maHP)
        {
            var result = from t1 in DbContext.tblDiemHocPhans
                         join t2 in DbContext.tblSinhviens on t1.MaSV equals t2.MaSV into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t4 in DbContext.tblSinhVienLopHocs on t1.MaSV equals t4.MaSV into x3
                         from t4 in x3.DefaultIfEmpty()
                         join t5 in DbContext.tblLophocs on t4.MaLop equals t5.MaLop into x4
                         from t5 in x4.DefaultIfEmpty()
                         join t3 in DbContext.tblHocPhans on t1.MaHP equals t3.MaHP into x2
                         from t3 in x2.DefaultIfEmpty()
                         orderby t1.NgayTao
                         select new
                         {
                             t1.MaDiem,
                             t1.MaSV,
                             t1.MaHP,
                             t1.Diem,
                             t1.HocKy,
                             t1.NamHoc,
                             t1.TinhTrang,
                             t1.SoLanHoc,
                             t1.SoTinChi,
                             t1.HeSo,
                             t1.DiemThanhPhan,
                             t1.GhiChu,
                             t1.NgayTao,
                             t2.HoVaTen,
                             t3.TenHocPhan,
                             t5.MaKhoaQuanLy,
                             t5.MaLop
                         };

            if (!string.IsNullOrEmpty(maSv))
                result = result.Where(x => x.MaSV.Contains(maSv));
            if (!string.IsNullOrEmpty(maPK))
                result = result.Where(x => x.MaHP.Contains(maPK));
            if (!string.IsNullOrEmpty(maLop))
                result = result.Where(x => x.MaHP.Contains(maLop));
            if (!string.IsNullOrEmpty(maHP))
                result = result.Where(x => x.MaHP.Contains(maHP));
            if (hocky != null)
                result = result.Where(x => x.HocKy == hocky);
            if (namhoc != null)
                result = result.Where(x => x.NamHoc == namhoc);

            if (result.Count() == 0)
                return new ApiResultPaging();

            int total = result.Count();
            int totalPage = (int)Math.Ceiling(total / (double)pagingParam.perPage);
            if (pagingParam.currentPage > total)
                pagingParam.currentPage = totalPage;
            result = result.Skip((pagingParam.currentPage - 1) * pagingParam.perPage).Take(pagingParam.perPage);
            ApiResultPaging apiResultPaging = new ApiResultPaging()
            {
                currentPage = pagingParam.currentPage,
                lastPage = totalPage,
                perPage = pagingParam.perPage,
                total = total,
                apiResult = result.ToList()
            };

            return apiResultPaging;
        }

        public ApiResultPaging DiemHocPhanChiTiet(PagingParam pagingParam, string maSv, string maHP)
        {
            var result = from t1 in DbContext.tblDiemHocPhanChiTiets
                         where t1.MaSV == maSv && t1.MaHP == maHP
                         orderby t1.NgayTao
                         select new
                         {
                             t1.ThuTu,
                             t1.MaSV,
                             t1.MaHP,
                             t1.Diem,
                             t1.HocKy,
                             t1.NamHoc,
                             t1.TinhTrang,
                             t1.DiemThanhPhan,
                             t1.NguoiDay
                         };
            if (!result.Any()) return new ApiResultPaging();

            int total = result.Count();
            int totalPage = (int)Math.Ceiling(total / (double)pagingParam.perPage);
            if (pagingParam.currentPage > total)
                pagingParam.currentPage = totalPage;
            result = result.Skip((pagingParam.currentPage - 1) * pagingParam.perPage).Take(pagingParam.perPage);
            ApiResultPaging apiResultPaging = new ApiResultPaging()
            {
                currentPage = pagingParam.currentPage,
                lastPage = totalPage,
                perPage = pagingParam.perPage,
                total = total,
                apiResult = result.ToList()
            };

            return apiResultPaging;
        }
        public ApiResultPaging SearchExt(PagingParam pagingParam, string maSv, string maHP)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand sqlComm = new SqlCommand("GettblDiemHocPhan", conn);
                sqlComm.Parameters.AddWithValue("@p_startRow", pagingParam.currentPage);
                sqlComm.Parameters.AddWithValue("@p_perPage", pagingParam.perPage);
                sqlComm.Parameters.AddWithValue("@p_MaSV", maSv);
                sqlComm.Parameters.AddWithValue("@p_MaHP", maHP);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(data);
            }

            int total = Convert.ToInt32(data.Tables[1].Rows[0]["total"]);
            int totalPage = Convert.ToInt32(data.Tables[1].Rows[0]["totalPage"]);

            List<Dto.DiemHocPhan> sv = new List<Dto.DiemHocPhan>();
            sv = (from DataRow dr in data.Tables[0].Rows
                  select new Dto.DiemHocPhan()
                  {
                      MaDiem = string.IsNullOrEmpty(dr["MaDiem"].ToString()) ? 0 : Convert.ToInt32(dr["MaDiem"].ToString()),
                      MaSV = dr["MaSV"].ToString(),
                      MaHP = dr["MaHP"].ToString(),
                      HoVaTen = dr["HoVaTen"].ToString(),
                      TenHocPhan = dr["TenHocPhan"].ToString(),
                      Diem = string.IsNullOrEmpty(dr["Diem"].ToString()) ? 0 : Convert.ToDouble(dr["Diem"].ToString()),
                      HeSo = string.IsNullOrEmpty(dr["HeSo"].ToString()) ? 0 : Convert.ToDouble(dr["HeSo"].ToString()),
                      SoLanHoc = string.IsNullOrEmpty(dr["SoLanHoc"].ToString()) ? 0 : Convert.ToInt32(dr["SoLanHoc"].ToString()),
                  }).ToList();
            if (sv.Count() == 0)
                return new ApiResultPaging();

            ApiResultPaging apiResultPaging = new ApiResultPaging()
            {
                currentPage = pagingParam.currentPage,
                lastPage = totalPage,
                perPage = pagingParam.perPage,
                total = total,
                apiResult = sv.ToList()
            };
            return apiResultPaging;
        }

        public ApiResultPaging SearhSinhVienHocLai(PagingParam pagingParam, string maSv, string maLop, string maNganh, int? hocKy, int? namHoc)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand sqlComm = new SqlCommand("GetSinhVienHocLai", conn);
                sqlComm.Parameters.AddWithValue("@p_startRow", pagingParam.currentPage);
                sqlComm.Parameters.AddWithValue("@p_perPage", pagingParam.perPage);
                sqlComm.Parameters.AddWithValue("@p_MaSV", maSv);
                sqlComm.Parameters.AddWithValue("@p_MaLop", maLop);
                sqlComm.Parameters.AddWithValue("@p_MaNganh", maNganh);
                sqlComm.Parameters.AddWithValue("@p_HocKy", hocKy);
                sqlComm.Parameters.AddWithValue("@p_NamHoc", namHoc);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(data);
            }

            int total = Convert.ToInt32(data.Tables[1].Rows[0]["total"]);
            int totalPage = Convert.ToInt32(data.Tables[1].Rows[0]["totalPage"]);

            List<Dto.DiemHocPhan> sv = new List<Dto.DiemHocPhan>();
            sv = (from DataRow dr in data.Tables[0].Rows
                  select new Dto.DiemHocPhan()
                  {
                      MaDiem = string.IsNullOrEmpty(dr["MaDiem"].ToString()) ? 0 : Convert.ToInt64(dr["MaDiem"].ToString()),
                      MaSV = dr["MaSV"].ToString(),
                      HoVaTen = dr["HoVaTen"].ToString(),
                      MaLop = dr["MaLop"].ToString(),
                      MaNganh = dr["MaNganh"].ToString(),
                      HocKy = string.IsNullOrEmpty(dr["HocKy"].ToString()) ? 0 : Convert.ToInt32(dr["HocKy"].ToString()),
                      NamHoc = string.IsNullOrEmpty(dr["NamHoc"].ToString()) ? 0 : Convert.ToInt32(dr["NamHoc"].ToString()),
                      SoLanHoc = string.IsNullOrEmpty(dr["SoLanHoc"].ToString()) ? 0 : Convert.ToInt32(dr["SoLanHoc"].ToString()),
                      SoTinChi = string.IsNullOrEmpty(dr["SoTinChi"].ToString()) ? 0 : Convert.ToInt32(dr["SoTinChi"].ToString()),
                      Diem = string.IsNullOrEmpty(dr["Diem"].ToString()) ? 0 : Convert.ToDouble(dr["Diem"].ToString()),
                      DiemThanhPhan = dr["DiemThanhPhan"].ToString(),
                      MaHP = dr["MaHP"].ToString(),
                      TenHocPhan = dr["TenHocPhan"].ToString(),
                  }).ToList();
            if (sv.Count() == 0)
                return new ApiResultPaging();

            ApiResultPaging apiResultPaging = new ApiResultPaging()
            {
                currentPage = pagingParam.currentPage,
                lastPage = totalPage,
                perPage = pagingParam.perPage,
                total = total,
                apiResult = sv.ToList()
            };
            return apiResultPaging;
        }
        public object KeHoachGiangDay()
        {
            var result = from t1 in DbContext.KeHoachGiangDays
                         where t1.trangthai == false
                         select new
                         {
                             t1.maKH,
                             t1.TenKeHoach,
                             t1.MaCBGV,
                             t1.MaHP,
                             t1.Hocky
                         };
            if (!result.Any()) return null;
            return result.ToList();
        }
        public object Detail(long maDiem)
        {
            var result = from t1 in DbContext.tblDiemHocPhans
                         join t2 in DbContext.tblSinhviens on t1.MaSV equals t2.MaSV into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblHocPhans on t1.MaHP equals t3.MaHP into x2
                         from t3 in x2.DefaultIfEmpty()
                         where t1.MaDiem == maDiem
                         select new
                         {
                             t1.MaDiem,
                             t1.MaSV,
                             t1.MaHP,
                             t1.Diem,
                             t1.HocKy,
                             t1.NamHoc,
                             t1.TinhTrang,
                             t1.SoLanHoc,
                             t1.SoTinChi,
                             t1.HeSo,
                             t1.DiemThanhPhan,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.MaKH,
                             t2.HoVaTen,
                             t3.TenHocPhan
                         };

            if (!result.Any()) return null;

            return result.SingleOrDefault();
        }

        public async Task<tblDiemHocPhan> Add(Dto.DiemHocPhan dhp)
        {
            tblKeHoachGiangDay t1 = await DbContext.KeHoachGiangDays.FindAsync(dhp.MaKH);
            tblHocPhan t2 = await DbContext.tblHocPhans.FindAsync(t1.MaHP);
            tblDiemHocPhan tbl = new tblDiemHocPhan()
            {
                MaSV = dhp.MaSV,
                MaKH = dhp.MaKH,
                MaHP = t2.MaHP,
                Diem = dhp.DiemKetThuc < 5 ? 0 : ((dhp.DiemChuyenCan + dhp.DiemBaiTap + dhp.DiemThucHanh) / 3 + dhp.DiemKetThuc) / 2,
                HocKy = t1.Hocky,
                SoTinChi = t2.SoTinChi,
                SoLanHoc = 1,
                NamHoc = dhp.NamHoc,
                DiemThanhPhan = dhp.DiemChuyenCan + ":" + dhp.DiemBaiTap + ":" + dhp.DiemThucHanh + ":" + dhp.DiemKetThuc,
                GhiChu = dhp.GhiChu,
                NgayTao = DateTime.Now,
                NguoiTao = "",
                TinhTrang = dhp.DiemKetThuc < 5 ? false : true
            };

            DbContext.tblDiemHocPhans.Add(tbl);
            await DbContext.SaveChangesAsync();

            return tbl;
        }

        public async Task<tblDiemHocPhanChiTiet> AddDetail(Dto.DiemHocPhan dhp)
        {
            tblKeHoachGiangDay t1 = await DbContext.KeHoachGiangDays.FindAsync(dhp.MaKH);
            tblHocPhan t2 = await DbContext.tblHocPhans.FindAsync(t1.MaHP);
            tblDiemHocPhanChiTiet tbl = new tblDiemHocPhanChiTiet()
            {
                ThuTu = 1,
                MaSV = dhp.MaSV,
                MaHP = t2.MaHP,
                Diem = dhp.DiemKetThuc < 5 ? 0 : ((dhp.DiemChuyenCan + dhp.DiemBaiTap + dhp.DiemThucHanh) / 3 + dhp.DiemKetThuc) / 2,
                HocKy = t1.Hocky,
                NamHoc = dhp.NamHoc,
                NguoiDay = t1.MaCBGV,
                DiemThanhPhan = dhp.DiemChuyenCan + ":" + dhp.DiemBaiTap + ":" + dhp.DiemThucHanh + ":" + dhp.DiemKetThuc,
                GhiChu = dhp.GhiChu,
                NgayTao = DateTime.Now,
                NguoiTao = "",
                TinhTrang = dhp.DiemKetThuc < 5 ? false : true
            };

            DbContext.tblDiemHocPhanChiTiets.Add(tbl);
            await DbContext.SaveChangesAsync();

            return tbl;
        }

        public async Task<tblDiemHocPhan> UpdateDiemHocLai(Dto.DiemHocPhan dhp)
        {
            tblKeHoachGiangDay t1 = await DbContext.KeHoachGiangDays.FindAsync(dhp.MaKH);
            tblHocPhan t2 = await DbContext.tblHocPhans.FindAsync(t1.MaHP);
            tblDiemHocPhan tbl = await DbContext.tblDiemHocPhans.FindAsync(dhp.MaDiem);

            if (tbl == null) return null;
            if (tbl.SoLanHoc > 3) return null;

            tbl.Diem = dhp.DiemKetThuc < 5 ? 0 : ((dhp.DiemChuyenCan + dhp.DiemBaiTap + dhp.DiemThucHanh) / 3 + dhp.DiemKetThuc) / 2;
            tbl.DiemThanhPhan = dhp.DiemChuyenCan + ":" + dhp.DiemBaiTap + ":" + dhp.DiemThucHanh + ":" + dhp.DiemKetThuc;
            tbl.GhiChu = dhp.GhiChu;
            tbl.SoLanHoc += 1;
            DbContext.Entry(tbl).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();

            tblDiemHocPhanChiTiet tblDetail = new tblDiemHocPhanChiTiet()
            {
                ThuTu = (int)tbl.SoLanHoc,
                MaSV = dhp.MaSV,
                MaHP = t2.MaHP,
                Diem = dhp.DiemKetThuc < 5 ? 0 : ((dhp.DiemChuyenCan + dhp.DiemBaiTap + dhp.DiemThucHanh) / 3 + dhp.DiemKetThuc) / 2,
                HocKy = t1.Hocky,
                NamHoc = dhp.NamHoc,
                NguoiDay = t1.MaCBGV,
                DiemThanhPhan = dhp.DiemChuyenCan + ":" + dhp.DiemBaiTap + ":" + dhp.DiemThucHanh + ":" + dhp.DiemKetThuc,
                GhiChu = dhp.GhiChu,
                NgayTao = DateTime.Now,
                NguoiTao = "",
                TinhTrang = dhp.DiemKetThuc < 5 ? false : true
            };

            DbContext.tblDiemHocPhanChiTiets.Add(tblDetail);
            await DbContext.SaveChangesAsync();
            return tbl;
        }

        public async Task<tblDiemHocPhan> Update(long maDiem, double diem, string ghichu)
        {
            tblDiemHocPhan dhp = await DbContext.tblDiemHocPhans.FindAsync(maDiem);
            dhp.Diem = diem;
            dhp.GhiChu = ghichu;
            DbContext.Entry(dhp).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            return dhp;
        }

        public async Task<tblDiemHocPhan> Delete(long maDiem)
        {
            tblDiemHocPhan dhp = await DbContext.tblDiemHocPhans.FindAsync(maDiem);
            DbContext.tblDiemHocPhans.Remove(dhp);
            await DbContext.SaveChangesAsync();
            return dhp;
        }
    }
}
