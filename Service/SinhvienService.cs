using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Common;
using Data;
using Data.Repositories;

namespace Service
{
    public class SinhvienService
    {

        private SinhvienRepository _sinhvienRepository = new SinhvienRepository();

        public async Task<ApiResult> Add(tblSinhvien sv)
        {
            try
            {
                sv.NgayTao = DateTime.Now;
                sv.NguoiTao = "";
                sv.MatKhau = "123123";
                sv.Quyen = 9;

                await _sinhvienRepository.Add(sv);
                return new ApiResult() { message = string.Empty, success = true, data = null };
            }
            catch { throw; }
        }
        public async Task<ApiResult> Update(tblSinhvien sv)
        {
            try
            {
                await _sinhvienRepository.Update(sv);
                return new ApiResult() { success = true, message = string.Empty, data = null };
            }
            catch { throw; }
        }
        public ApiResult Search(PagingParam pagingParam, string masv, string hovaten, string malop)
        {
            try
            {
                ApiResultPaging resultPaging = _sinhvienRepository.SearchExt(pagingParam, masv, hovaten, malop);
                return new ApiResult() { success = true, message = string.Empty, data = resultPaging };
            }
            catch { throw; }
        }
        public ApiResult Infomation(string masv)
        {
            try
            {
                tblSinhvien svResult = _sinhvienRepository.Infomation(masv);
                return new ApiResult() { success = true, message = string.Empty, data = svResult };
            }
            catch { throw; }
        }
        public ApiResult Detail(string masv)
        {
            try
            {
                object result = _sinhvienRepository.Detail(masv);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public async Task<ApiResult> Delete(string masv)
        {
            try
            {
                await _sinhvienRepository.Delete(masv);
                return new ApiResult() { success = true, message = string.Empty, data = null };
            }
            catch { throw; }
        }

        public async Task<ApiResult> ImportFile(DataTable data)
        {
            try
            {
                var result = await _sinhvienRepository.ImportFile(data);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }
    }
}
