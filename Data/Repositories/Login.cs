using System;
using System.Linq;

namespace Data.Repositories
{
    public static class Login
    {
        static ApplicationDbContext DbContext = new ApplicationDbContext();
        public static object GetLogin(string ID, string matKhau)
        {
            var result = DbContext.tblCanBoGiangViens.Where(x => String.Compare(x.MaCBGV.ToString(), ID) == 0 && String.Compare(x.MatKhau.ToString(), matKhau) == 0).Select(x => new
            {
                ID = x.MaCBGV,
                x.HoVaTen,
                x.Quyen//1//8
            }).FirstOrDefault();
            if (result != null)
                return result;
            var resultsv = DbContext.tblSinhviens.Where(x => x.MaSV.ToString() == ID && x.MatKhau.ToString() == matKhau).Select(x => new
            {
                ID = x.MaSV,
                x.HoVaTen,
                x.Quyen//9
            }).FirstOrDefault();

            //var resultsv = DbContext.tblSinhviens.Where(x => String.Compare(x.MaSV.ToString(), ID) == 0 && String.Compare(x.MatKhau.ToString(), matKhau) == 0).Select(x => new
            //{
            //    ID = x.MaSV,
            //    x.HoVaTen,
            //    x.Quyen//9
            //}).FirstOrDefault();
            if (resultsv != null)
                return resultsv;
            return null;
        }
    }
}
