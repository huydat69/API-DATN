namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSinhVienLopHoc")]
    public partial class tblSinhVienLopHoc
    {
        [Key]
        public long MaSVLH { get; set; }

        [StringLength(10)]
        public string MaSV { get; set; }

        [StringLength(10)]
        public string MaLop { get; set; }

        public int? HoatDong { get; set; }

        public DateTime? NgayVaoLop { get; set; }

        [Column(TypeName = "text")]
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
