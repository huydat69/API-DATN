using System;
using System.Data.Entity;
using System.Linq;
using Common;

namespace Data.Repositories
{
    public class NganhDaoTaoRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();

        public ApiResultPaging Search(PagingParam pagingParam, string maNganh, string tenNganh)
        {
            var result = from t1 in DbContext.tblNganhDaoTaos
                         join t2 in DbContext.tblPhongKhoas on t1.MaPK equals t2.MaPK into x1
                         from t2 in x1.DefaultIfEmpty()
                         orderby t1.NgayTao descending
                         select new
                         {
                             t1.MaPK,
                             t1.MaNganh,
                             t1.MaBMTT,
                             t1.MaNganhTS,
                             t1.TenNganh,
                             t1.SoTinChi,
                             t1.TrinhDo,
                             t1.SoThang,
                             t1.NamTS,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.NguoiTao,
                             t2.TenPhongKhoa,
                         };

            if (!string.IsNullOrEmpty(maNganh))
                result = result.Where(x => x.MaNganh.Contains(maNganh));

            if (!string.IsNullOrEmpty(tenNganh))
                result = result.Where(x => x.TenNganh.Contains(tenNganh));

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

        public object Detail(string maNghanh)
        {
            if (string.IsNullOrEmpty(maNghanh)) return null;

            var result = from t1 in DbContext.tblNganhDaoTaos
                         join t2 in DbContext.tblPhongKhoas on t1.MaPK equals t2.MaPK into x1
                         from t2 in x1.DefaultIfEmpty()
                         where string.Compare(t1.MaNganh, maNghanh) == 0
                         orderby t1.NgayTao descending
                         select new
                         {
                             t1.MaPK,
                             t1.MaNganh,
                             t1.MaBMTT,
                             t1.MaNganhTS,
                             t1.TenNganh,
                             t1.SoTinChi,
                             t1.TrinhDo,
                             t1.SoThang,
                             t1.NamTS,
                             t1.GhiChu,
                             t1.NgayTao,
                             t1.NguoiTao,
                             t2.TenPhongKhoa
                         };

            if (!result.Any()) return null;

            return result.SingleOrDefault();
        }
    }
}
