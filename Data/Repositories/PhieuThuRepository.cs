using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Common;

namespace Data.Repositories
{
    public class PhieuThuRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        public ApiResultPaging DanhSachPhieuThu(PagingParam pagingParam, string maSv, string soPhieu)
        {
            var result = from t1 in DbContext.tblPhieuThus
                         join t2 in DbContext.tblSinhviens on t1.NguoiNop equals t2.MaSV into x1
                         from t2 in x1.DefaultIfEmpty()
                         orderby t2.NgayTao
                         select new
                         {
                             t2.MaSV,
                             t2.HoVaTen,
                             t1.MaPhieuThu,
                             t1.SoPhieu,
                             t1.MoTa,
                             t1.Ngay,
                             t1.NguoiThu,
                             t1.TongTien,
                             HoaDonDienTu = t1.HoaDonDienTu == true ? "Là hóa đơn điện tử" : "Hóa đơn thường",
                             t1.GhiChu,
                         };
            if (!string.IsNullOrEmpty(maSv))
                result = result.Where(x => x.MaSV.Contains(maSv));
            if (!string.IsNullOrEmpty(soPhieu))
                result = result.Where(x => x.SoPhieu == soPhieu);
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

        public ApiResultPaging ChiTietPhieuThu(PagingParam pagingParam, int maPhieuThu)
        {
            var result = from t1 in DbContext.tblChiTietPhieuThus
                         join t2 in DbContext.tblPhieuThus on t1.MaPhieuThu equals t2.MaPhieuThu into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblKhoanThus on t1.MaKhoanThu equals t3.MaKhoanThu into x2
                         from t3 in x2.DefaultIfEmpty()
                         orderby t1.NgayTao
                         where t1.MaPhieuThu == maPhieuThu
                         select new
                         {
                             t1.MaKhoanThu,
                             t3.MoTa,
                             t1.SoTienThu,
                             t1.SoTienNop,
                             t1.SoTienThuaThieu
                         };

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
    }
}
