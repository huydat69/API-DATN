namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblLophoc")]
    public partial class tblLophoc
    {
        [Key]
        [StringLength(10)]
        public string MaLop { get; set; }

        [StringLength(10)]
        public string TenLop { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNganhHoc { get; set; }

        [StringLength(10)]
        public string MaKhoaQuanLy { get; set; }

        [StringLength(10)]
        public string NienKhoa { get; set; }

        public int? TrinhDo { get; set; }

        public int? He { get; set; }

        public DateTime? NgayNhapHoc { get; set; }

        public int? SiSo { get; set; }

        public int? TrangThai { get; set; }

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
