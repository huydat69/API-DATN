using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Data.Repositories
{
    public class LophocRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();

        public ApiResultPaging Search(PagingParam pagingParam, string maLop, string tenLop, string maPK)
        {
            var result = from t1 in DbContext.tblLophocs
                         join t2 in DbContext.tblPhongKhoas on t1.MaKhoaQuanLy equals t2.MaPK into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblNganhDaoTaos on t1.MaNganhHoc equals t3.MaNganh into x2
                         from t3 in x2.DefaultIfEmpty()
                         orderby t1.NgayTao descending
                         select new
                         {
                             t1.MaLop,
                             t1.TenLop,
                             t1.MaNganhHoc,
                             t1.MaKhoaQuanLy,
                             t1.NienKhoa,
                             t1.TrinhDo,
                             t1.He,
                             t1.NgayNhapHoc,
                             t1.SiSo,
                             t1.TrangThai,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.NguoiTao,
                             t2.MaPK,
                             t2.TenPhongKhoa,
                             t3.TenNganh
                         };

            if (!string.IsNullOrEmpty(maLop))
                result = result.Where(x => x.MaLop.Contains(maLop));

            if (!string.IsNullOrEmpty(tenLop))
                result = result.Where(x => x.TenLop.Contains(tenLop));

            if (!string.IsNullOrEmpty(maPK))
                result = result.Where(x => x.MaPK.Contains(maPK));

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

        public ApiResultPaging SinhvienLopHoc(PagingParam pagingParam, string maLop)
        {
            var result = from t1 in DbContext.tblSinhviens
                         join t2 in DbContext.tblSinhVienLopHocs on t1.MaSV equals t2.MaSV into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblLophocs on t2.MaLop equals t3.MaLop into x2
                         from t3 in x2.DefaultIfEmpty()
                         where t1.TrangThai != 0 && t2.MaLop == maLop
                         orderby t1.NgayTao descending
                         select new
                         {
                             t1.MaSV,
                             t1.HoVaTen,
                             t1.NgaySinh,
                             t1.GioiTinh,
                             t1.DanToc,
                             t1.SoDinhDanh,
                             t1.NoiCap,
                             t1.NgayCap,
                             t1.DienThoai,
                             t1.Email,
                             t1.MatKhau,
                             t1.TrangThai,
                             t1.NgayTao,
                             t1.NguoiTao,
                             t2.MaLop
                         };

            int total = result.Count();
            int totalPage = (int)Math.Ceiling(total / (double)pagingParam.perPage);

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

        public object Detail(string maLop)
        {
            if (string.IsNullOrEmpty(maLop)) return null;

            var result = from t1 in DbContext.tblLophocs
                         join t2 in DbContext.tblPhongKhoas on t1.MaKhoaQuanLy equals t2.MaPK into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblNganhDaoTaos on t1.MaNganhHoc equals t3.MaNganh into x2
                         from t3 in x2.DefaultIfEmpty()
                         where string.Compare(t1.MaLop, maLop) == 0
                         orderby t1.NgayTao descending
                         select new
                         {
                             t1.MaLop,
                             t1.TenLop,
                             t1.MaNganhHoc,
                             t1.MaKhoaQuanLy,
                             t1.NienKhoa,
                             t1.TrinhDo,
                             t1.He,
                             t1.NgayNhapHoc,
                             t1.SiSo,
                             t1.TrangThai,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.NguoiTao,
                             t2.TenPhongKhoa,
                             t3.TenNganh
                         };

            if (!result.Any()) return null;

            return result.SingleOrDefault();
        }

        public async Task<bool> AddSinhVienLopHoc(string maSv, string maLop)
        {
            if (string.IsNullOrEmpty(maSv))
                return false;

            if (!DbContext.tblSinhviens.Any(x => x.MaSV == maSv) || !DbContext.tblLophocs.Any(x => x.MaLop == maLop))
                return false;

            if (DbContext.tblSinhVienLopHocs.Any(x => x.MaLop == maLop && x.MaSV == maSv))
                return false;

            tblSinhVienLopHoc svlh = new tblSinhVienLopHoc()
            {
                MaSV = maSv,
                MaLop = maLop,
                HoatDong = 1,
                NgayVaoLop = DateTime.Now,
                GhiChu = null,
                NgayTao = DateTime.Now,
                NguoiTao = ""
            };

            try
            {
                DbContext.tblSinhVienLopHocs.Add(svlh);
                await DbContext.SaveChangesAsync();
            }
            catch { return false; }

            return true;
        }
    }
}
