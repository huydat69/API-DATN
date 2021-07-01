namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCanBoGiangVien")]
    public partial class tblCanBoGiangVien
    {
        [Key]
        [StringLength(10)]
        public string MaCBGV { get; set; }

        [StringLength(10)]
        public string MaPK { get; set; }

        [StringLength(10)]
        public string MaBMTT { get; set; }

        [StringLength(50)]
        public string HoVaTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        public int? GioiTinh { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        [Column(TypeName = "ntext")]
        public string DienThoai { get; set; }

        [Column(TypeName = "ntext")]
        public string Email { get; set; }

        [StringLength(10)]
        public string ChucDanh { get; set; }

        [Column(TypeName = "text")]
        public string SoTaiKhoan { get; set; }

        public int? TrangThai { get; set; }

        public int? Quyen { get; set; }

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
