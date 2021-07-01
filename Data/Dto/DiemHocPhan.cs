using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class DiemHocPhan
    {
        public long MaDiem { get; set; }
        public string MaSV { get; set; }
        public string MaHP { get; set; }
        public long MaKH { get; set; }
        public double? Diem { get; set; }
        public int? HocKy { get; set; }
        public int? NamHoc { get; set; }
        public int? TinhTrang { get; set; }
        public int? ThuTu { get; set; }
        public int? SoLanHoc { get; set; }
        public double? SoTinChi { get; set; }
        public double? HeSo { get; set; }
        public int? SoThuTu { get; set; }
        public int? TinhTrungBinh { get; set; }
        public int? TotNghiep { get; set; }
        public string DiemThanhPhan { get; set; }
        public string GhiChu { get; set; }
        public DateTime? NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public string DP1 { get; set; }
        public string DP2 { get; set; }
        public string DP3 { get; set; }
        public string HoVaTen { get; set; }
        public string TenHocPhan { get; set; }
        public string MaNganh { get; set; }
        public string MaLop { get; set; }
        public double DiemChuyenCan { get; set; }
        public double DiemBaiTap { get; set; }
        public double DiemThucHanh { get; set; }
        public double DiemKetThuc { get; set; }
    }
}
