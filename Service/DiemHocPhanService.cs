using System;
using Common;
using Data;
using Data.Repositories;
using System.Threading.Tasks;

namespace Service
{
    public class DiemHocPhanService
    {
        private DiemHocPhanRepository _diemHocPhanRepository = new DiemHocPhanRepository();

        public ApiResult Search(PagingParam pagingParam, string maSv, string maPK, string maLop, string maHP)
        {
            try
            {
                object result = _diemHocPhanRepository.Search(pagingParam, maSv, null, null, maPK, maLop, maHP);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult DiemHocPhanChiTiet(PagingParam pagingParam, string maSv, string maHP)
        {
            try
            {
                if (string.IsNullOrEmpty(maSv) || string.IsNullOrEmpty(maHP))
                    return new ApiResult() { success = false, message = string.Empty, data = null };

                object result = _diemHocPhanRepository.DiemHocPhanChiTiet(pagingParam, maSv, maHP);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult SearhSinhVienHocLai(PagingParam pagingParam, string maSv, string maLop, string maNganh, int? hocKy, int? namHoc)
        {
            try
            {
                object result = _diemHocPhanRepository.SearhSinhVienHocLai(pagingParam, maSv, maLop, maNganh, hocKy, namHoc);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult KeHoachGiangDay()
        {
            try
            {
                object result = _diemHocPhanRepository.KeHoachGiangDay();
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public ApiResult Detail(long maDiem)
        {
            try
            {
                object result = _diemHocPhanRepository.Detail(maDiem);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public async Task<ApiResult> Add(Data.Dto.DiemHocPhan dhp)
        {
            try
            {
                tblDiemHocPhan result = await _diemHocPhanRepository.Add(dhp);
                await _diemHocPhanRepository.AddDetail(dhp);
                return new ApiResult() { message = string.Empty, success = result != null ? true : false, data = result };
            }
            catch { throw; }
        }

        public async Task<ApiResult> UpdateDiemHocLai(Data.Dto.DiemHocPhan dhp)
        {
            try
            {
                tblDiemHocPhan result = await _diemHocPhanRepository.UpdateDiemHocLai(dhp);
                return new ApiResult() { message = string.Empty, success = result != null ? true : false, data = result };
            }
            catch { throw; }
        }

        public async Task<ApiResult> Update(long maDiem, double diem, string ghichu)
        {
            try
            {
                tblDiemHocPhan result = await _diemHocPhanRepository.Update(maDiem, diem, ghichu);
                return new ApiResult() { success = result != null ? true : false, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public async Task<ApiResult> Delete(long maDiem)
        {
            try
            {
                tblDiemHocPhan result = await _diemHocPhanRepository.Delete(maDiem);
                return new ApiResult() { success = result != null ? true : false, message = string.Empty, data = result };
            }
            catch { throw; }
        }
    }
}
