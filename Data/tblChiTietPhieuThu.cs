namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblChiTietPhieuThu")]
    public partial class tblChiTietPhieuThu
    {
        [Key]
        public long MaChiTietPhieuThu { get; set; }

        public long? MaPhieuThu { get; set; }

        [StringLength(10)]
        public string MaKhoanThu { get; set; }

        public decimal? SoTienThu { get; set; }

        public decimal? SoTienNop { get; set; }

        public decimal? SoTienThuaThieu { get; set; }

        public bool? HoaDonDienTu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
    }
}
