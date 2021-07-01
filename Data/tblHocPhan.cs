namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblHocPhan")]
    public partial class tblHocPhan
    {
        [Key]
        [StringLength(10)]
        public string MaHP { get; set; }

        [StringLength(10)]
        public string MaPK { get; set; }

        [StringLength(10)]
        public string MaBMTT { get; set; }

        [Column(TypeName = "ntext")]
        public string TenHocPhan { get; set; }

        public int? HocKy { get; set; }

        public int? TinhChat { get; set; }

        public double? SoTinChi { get; set; }

        public double? SoTinChiLT { get; set; }

        public double? SoTinChiTH { get; set; }

        public double? HeSo { get; set; }

        public int? SoThuTu { get; set; }

        public int? TinhTrungBinh { get; set; }

        public int? TotNghiep { get; set; }

        [Column(TypeName = "ntext")]
        public string DiemThanhPhan { get; set; }

        [Column(TypeName = "ntext")]
        public string NganhApDung { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        [Column(TypeName = "ntext")]
        public string DP1 { get; set; }

        [Column(TypeName = "ntext")]
        public string DP2 { get; set; }

        [Column(TypeName = "ntext")]
        public string DP3 { get; set; }
    }
}
