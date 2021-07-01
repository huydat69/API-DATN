using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Data;
using Data.Repositories;
using System.Linq;

namespace Service
{
    public class ClientService
    {
        private ClientRepository _clientRepository = new ClientRepository();
        private SinhvienRepository _sinhvienRepository = new SinhvienRepository();
        private DiemHocPhanRepository _diemHocPhanRepository = new DiemHocPhanRepository();
        public ApiResult TraCuuDiem(string maSV, string maLop, string maHP)
        {
            try
            {
                if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(maLop)
                    || string.IsNullOrEmpty(maHP))
                    return null;
                object result = _clientRepository.TraCuuDiem(maSV, maLop, maHP);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public ApiResult TraCuuKhoanThu(string maKhoanThu)
        {
            try
            {
                if (string.IsNullOrEmpty(maKhoanThu))
                    return null;
                object result = _clientRepository.TraCuuKhoanThu(maKhoanThu);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public ApiResult ThongTinCaNhanSinhVien(string maSv)
        {
            try
            {
                if (string.IsNullOrEmpty(maSv))
                    return null;
                object result = _sinhvienRepository.Infomation(maSv);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public ApiResult DiemThiSinhVien(PagingParam pagingParam, string maSv, int? hocky, int? namhoc)
        {
            try
            {
                object result = _diemHocPhanRepository.Search(pagingParam, maSv, hocky, namhoc, null, null, null);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult KhoanThuSinhVien(string maSv)
        {
            try
            {
                if (string.IsNullOrEmpty(maSv))
                    return null;
                object result = _clientRepository.KhoanThuSinhVien(maSv);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public async Task<ApiResult> TaoKhoanThuSinhVien(string maLop, string MaKhoanThuStr)
        {
            if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(MaKhoanThuStr))
                return new ApiResult() { success = false, message = string.Empty, data = null };

            try
            {
                List<tblKhoanThuSinhVien> result = new List<tblKhoanThuSinhVien>();

                string[] MaKhoanThuArray = MaKhoanThuStr.Split('|');
                var sinhvienList = _clientRepository.SinhVienLopHoc(maLop.Trim());

                foreach (var sv in sinhvienList)
                {
                    foreach (string makhoanthu in MaKhoanThuArray)
                    {
                        tblKhoanThuSinhVien tbl = new tblKhoanThuSinhVien()
                        {
                            MaKhoanThu = makhoanthu.Trim(),
                            MaSV = sv.MaSV,
                            TrangThai = false,
                            NgayTao = DateTime.Now
                        };
                        var ktsv = await _clientRepository.TaoKhoanThuSinhVien(tbl);
                        if (ktsv != null)
                            result.Add(ktsv);
                    }
                }

                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public async Task<ApiResult> TaoPhieuThuSinhVien(string maLop)
        {
            if (string.IsNullOrEmpty(maLop))
                return new ApiResult() { success = false, message = string.Empty, data = null };

            try
            {
                List<tblPhieuThu> resultPT = new List<tblPhieuThu>();
                List<tblChiTietPhieuThu> resultCTPT = new List<tblChiTietPhieuThu>();

                var sinhvienList = _clientRepository.SinhVienLopHoc(maLop.Trim());

                foreach (var sv in sinhvienList)
                {
                    var KTSV = _clientRepository.IEKhoanThuSinhVien(sv.MaSV);

                    tblPhieuThu tblPT = new tblPhieuThu()
                    {
                        SoPhieu = System.Guid.NewGuid().ToString(),
                        MaGiaoDich = string.Empty,
                        Ngay = DateTime.Now,
                        NgayTao = DateTime.Now,
                        MoTa = string.Empty,
                        NguoiNop = sv.MaSV,
                        NguoiThu = "02031",
                        MaNguoiThu = "02031",
                        TongTien = 0,
                        HoaDonDienTu = false
                    };

                    tblPhieuThu pt = await _clientRepository.TaoPhieuThu(tblPT);
                    resultPT.Add(pt);

                    foreach (var kt in KTSV)
                    {
                        if (_clientRepository.Check_CTPhieuthu(sv.MaSV, kt.MaKhoanThu))
                        {
                            tblChiTietPhieuThu tblCTPT = new tblChiTietPhieuThu()
                            {
                                MaPhieuThu = pt.MaPhieuThu,
                                MaKhoanThu = kt.MaKhoanThu,
                                SoTienThu = _clientRepository.Khoanthu_Detail(kt.MaKhoanThu).SoTien,
                                SoTienNop = _clientRepository.Khoanthu_Detail(kt.MaKhoanThu).SoTien,
                                SoTienThuaThieu = 0,
                                NgayTao = DateTime.Now
                            };

                            var ktsv = await _clientRepository.TaoChiTietPhieuThu(tblCTPT);
                            if (ktsv != null)
                                resultCTPT.Add(tblCTPT);
                        }   
                    }
                }

                var result = from t1 in resultPT
                             join t2 in resultCTPT on t1.MaPhieuThu equals t2.MaPhieuThu
                             select new
                             {
                                 t1.MaPhieuThu,
                                 t1.NguoiNop,
                                 t1.NguoiThu,
                                 t2.MaKhoanThu,
                                 t2.SoTienNop,
                                 t2.SoTienThu,
                                 t2.SoTienThuaThieu
                             };

                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }
    }
}
