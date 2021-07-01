using System;
using Common;
using Data;
using Data.Repositories;

namespace Service
{
    public class PhieuThuService
    {
        private PhieuThuRepository _phieuthuRepository = new PhieuThuRepository();

        public ApiResult DanhSachPhieuThu(PagingParam pagingParam, string maSv, string soPhieu)
        {
            try
            {
                object result = _phieuthuRepository.DanhSachPhieuThu(pagingParam, maSv, soPhieu);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }
        public ApiResult ChiTietPhieuThu(PagingParam pagingParam, int maPhieuThu)
        {
            try
            {
                object result = _phieuthuRepository.ChiTietPhieuThu(pagingParam, maPhieuThu);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }
    }
}
