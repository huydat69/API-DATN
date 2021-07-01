using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CanBoGiangVienRepository
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();
        public tblCanBoGiangVien GetCanBoGiangVien(string maCBGV, string matKhau)
        {
            var result = DbContext.tblCanBoGiangViens.Where(x => x.MaCBGV == maCBGV && x.MatKhau == matKhau).FirstOrDefault();
            return result;
        }
    }
}
