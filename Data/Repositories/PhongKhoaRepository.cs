using System;
using System.Data.Entity;
using System.Linq;
using Common;

namespace Data.Repositories
{
    public class PhongKhoaRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();

        public ApiResultPaging Search(PagingParam pagingParam, string maPK, string tenPhongKhoa)
        {
            var result = from t1 in DbContext.tblPhongKhoas
                         orderby t1.NgayTao descending
                         select new
                         {
                             t1.MaPK,
                             t1.TenPhongKhoa,
                             t1.SoLuongNhanSu,
                             t1.PhanLoai,
                             t1.DiaChi,
                             t1.DienThoai,
                             t1.Email,
                             t1.Webiste,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.NguoiTao
                         };

            if (!string.IsNullOrEmpty(maPK))
                result = result.Where(x => x.MaPK.Contains(maPK));

            if (!string.IsNullOrEmpty(tenPhongKhoa))
                result = result.Where(x => x.TenPhongKhoa.Contains(tenPhongKhoa));

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

        public object Detail(string maPK)
        {
            if (string.IsNullOrEmpty(maPK)) return null;

            var result = from t1 in DbContext.tblPhongKhoas
                         orderby t1.NgayTao descending
                         where string.Compare(t1.MaPK, maPK) == 0
                         select new
                         {
                             t1.MaPK,
                             t1.TenPhongKhoa,
                             t1.SoLuongNhanSu,
                             t1.PhanLoai,
                             t1.DiaChi,
                             t1.DienThoai,
                             t1.Email,
                             t1.Webiste,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.NguoiTao
                         };

            if (!result.Any()) return null;

            return result.SingleOrDefault();
        }
    }
}
