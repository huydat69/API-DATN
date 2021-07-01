namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDinhMucHocPhi")]
    public partial class tblDinhMucHocPhi
    {
        [Key]
        public long MaDMHP { get; set; }

        [StringLength(10)]
        public string MaLop { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HocPhiThang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HocPhiHocKy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HocPhiTinChi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HocPhiTinChiLT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? HocPhiTinChiTH { get; set; }

        public int? HocKy { get; set; }

        public int? NamHoc { get; set; }

        public int? TrangThai { get; set; }

        public int? TinhChat { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        [Column(TypeName = "ntext")]
        public string QuyetDinh { get; set; }

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

        [StringLength(10)]
        public string MaKhoanThu { get; set; }
    }
}
