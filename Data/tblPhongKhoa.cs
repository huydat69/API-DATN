namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblPhongKhoa")]
    public partial class tblPhongKhoa
    {
        [Key]
        [StringLength(10)]
        public string MaPK { get; set; }

        [StringLength(150)]
        public string TenPhongKhoa { get; set; }

        public int? SoLuongNhanSu { get; set; }

        public int? PhanLoai { get; set; }

        [Column(TypeName = "ntext")]
        public string DiaChi { get; set; }

        [Column(TypeName = "ntext")]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "ntext")]
        public string Webiste { get; set; }

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
