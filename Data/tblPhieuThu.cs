namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPhieuThu")]
    public partial class tblPhieuThu
    {
        [Key]
        public long MaPhieuThu { get; set; }

        public string SoPhieu { get; set; }

        [StringLength(20)]
        public string MaGiaoDich { get; set; }

        public DateTime? Ngay { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string NguoiNop { get; set; }

        [StringLength(50)]
        public string NguoiThu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNguoiThu { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TongTien { get; set; }

        public bool? HoaDonDienTu { get; set; }

        [Column(TypeName = "text")]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
    }
}
