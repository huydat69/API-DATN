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
    public class SinhvienRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
        public ApiResultPaging Search(PagingParam pagingParam, string masv, string hovaten, string malop)
        {
            var result = from t1 in DbContext.tblSinhviens
                         join t2 in DbContext.tblSinhVienLopHocs on t1.MaSV equals t2.MaSV into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblLophocs on t2.MaLop equals t3.MaLop into x2
                         from t3 in x2.DefaultIfEmpty()
                         where t1.TrangThai != 0
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
            if (!string.IsNullOrEmpty(masv))
                result = result.Where(x => x.MaSV.Contains(masv));

            if (!string.IsNullOrEmpty(hovaten))
                result = result.Where(x => x.HoVaTen.Contains(hovaten));

            if (!string.IsNullOrEmpty(malop))
                result = result.Where(x => x.MaLop.Contains(malop));

            int total = result.Count();
            if (total <= 0)
                return new ApiResultPaging();

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
        public ApiResultPaging SearchExt(PagingParam pagingParam, string masv, string hovaten, string malop)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand sqlComm = new SqlCommand("GettblSinhvien", conn);
                sqlComm.Parameters.AddWithValue("@p_startRow", pagingParam.currentPage);
                sqlComm.Parameters.AddWithValue("@p_perPage", pagingParam.perPage);
                sqlComm.Parameters.AddWithValue("@p_MaSV", masv);
                sqlComm.Parameters.AddWithValue("@p_HoVaTen", hovaten);
                sqlComm.Parameters.AddWithValue("@p_MaLop", malop);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(data);
            }

            int total = Convert.ToInt32(data.Tables[1].Rows[0]["total"]);
            int totalPage = Convert.ToInt32(data.Tables[1].Rows[0]["totalPage"]);

            List<Dto.Sinhvien> sv = new List<Dto.Sinhvien>();
            sv = (from DataRow dr in data.Tables[0].Rows
                  select new Dto.Sinhvien()
                  {
                      MaSV = dr["MaSV"].ToString(),
                      HoVaTen = dr["HoVaTen"].ToString(),
                      NgaySinh = string.IsNullOrEmpty(dr["NgaySinh"].ToString()) ? DateTime.Now : DateTime.Parse(dr["NgaySinh"].ToString()),
                      GioiTinh = string.IsNullOrEmpty(dr["GioiTinh"].ToString()) ? 0 : Convert.ToInt32(dr["GioiTinh"].ToString()),
                      DanToc = dr["DanToc"].ToString()
                  }).ToList();
            if (sv.Count() <= 0)
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
        public tblSinhvien Infomation(string masv)
        {
            tblSinhvien result = DbContext.tblSinhviens.Where(x => x.MaSV == masv).SingleOrDefault();
            return result;
        }
        public object Detail(string masv)
        {
            if (string.IsNullOrEmpty(masv))
                return null;

            var result = from t1 in DbContext.tblSinhviens
                         join t2 in DbContext.tblSinhVienLopHocs on t1.MaSV equals t2.MaSV into x1
                         from t2 in x1.DefaultIfEmpty()
                         join t3 in DbContext.tblLophocs on t2.MaLop equals t3.MaLop into x2
                         from t3 in x2.DefaultIfEmpty()
                         where t1.MaSV == masv
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
                             t1.TrangThai,
                             t1.NgayTao,
                             t1.NguoiTao,
                             t2.MaLop
                         };

            if (!result.Any())
                return null;

            return result;
        }

        public async Task Add(tblSinhvien sv)
        {
            DbContext.tblSinhviens.Add(sv);
            await DbContext.SaveChangesAsync();
        }

        public async Task Update(tblSinhvien sv)
        {
            DbContext.Entry(sv).State = EntityState.Modified;
            DbContext.Entry(sv).Property(x => x.NgayTao).IsModified = false;
            DbContext.Entry(sv).Property(x => x.MatKhau).IsModified = false;
            DbContext.Entry(sv).Property(x => x.Quyen).IsModified = false;
            DbContext.Entry(sv).Property(x => x.AnhThe).IsModified = false;
            DbContext.Entry(sv).Property(x => x.NguoiTao).IsModified = false;
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(string masv)
        {
            tblSinhvien sv = await DbContext.tblSinhviens.FindAsync(masv);
            DbContext.tblSinhviens.Remove(sv);
            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<tblSinhvien>> ImportFile(DataTable data)
        {
            data.Rows.Remove(data.Rows[0]);
            IEnumerable<tblSinhvien> sv = new List<tblSinhvien>();
            sv = (from DataRow dr in data.Rows
                  select new tblSinhvien()
                  {
                      MaSV = dr[0].ToString(),
                      HoVaTen = dr[1].ToString(),
                      NgaySinh = string.IsNullOrEmpty(dr[2].ToString()) || dr[2].ToString().Contains("NULL") ? DateTime.Now : DateTime.Parse(dr[2].ToString()),
                      GioiTinh = string.IsNullOrEmpty(dr[3].ToString()) || dr[3].ToString().Contains("NULL") ? 0 : Convert.ToInt32(dr[3].ToString()),
                      DanToc = dr[4].ToString(),
                      SoDinhDanh = dr[5].ToString(),
                      NoiCap = dr[6].ToString(),
                      NgayCap = string.IsNullOrEmpty(dr[7].ToString()) || dr[7].ToString().Contains("NULL") ? DateTime.Now : DateTime.Parse(dr[7].ToString()),
                      DienThoai = dr[8].ToString(),
                      Email = dr[9].ToString(),
                      MatKhau = dr[10].ToString(),
                      QueQuan = dr[11].ToString(),
                      NoiThuongTru = dr[12].ToString(),
                      Quyen = 9,
                      TrangThai = string.IsNullOrEmpty(dr[13].ToString()) || dr[13].ToString().Contains("NULL") ? 0 : Convert.ToInt32(dr[13].ToString()),
                      NgayTao = !string.IsNullOrEmpty(dr[14].ToString()) || dr[14].ToString().Contains("NULL") ? DateTime.Parse(dr[14].ToString()) : DateTime.Now,
                      NguoiTao = dr[15].ToString()
                  }).AsEnumerable();

            sv = sv.Where(x => !String.IsNullOrEmpty(x.MaSV));
            DbContext.tblSinhviens.AddRange(sv);
            await DbContext.SaveChangesAsync();

            return sv;
        }
    }
}
