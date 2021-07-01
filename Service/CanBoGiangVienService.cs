using Data;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CanBoGiangVienService
    {
        private CanBoGiangVienRepository _canBoGiangVienRepository = new CanBoGiangVienRepository();
        public tblCanBoGiangVien GetCanBoGiangVien(string maCBGV, string matKhau)
        {
            return _canBoGiangVienRepository.GetCanBoGiangVien(maCBGV, matKhau);
        }
    }
}
