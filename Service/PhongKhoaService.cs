using System;
using Common;
using Data;
using Data.Repositories;

namespace Service
{
    public class PhongKhoaService
    {
        private PhongKhoaRepository _phongKhoaRepository = new PhongKhoaRepository();

        public ApiResult Search(PagingParam pagingParam, string maPK, string tenPhongKhoa)
        {
            try
            {
                object result = _phongKhoaRepository.Search(pagingParam, maPK, tenPhongKhoa);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult Detail(string maPK)
        {
            try
            {
                object result = _phongKhoaRepository.Detail(maPK);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }
    }
}
