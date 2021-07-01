namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblNganhDaoTao")]
    public partial class tblNganhDaoTao
    {
        [Key]
        [StringLength(10)]
        public string MaNganh { get; set; }

        [StringLength(10)]
        public string MaPK { get; set; }

        [StringLength(10)]
        public string MaBMTT { get; set; }

        [Column(TypeName = "ntext")]
        public string MaNganhTS { get; set; }

        [Column(TypeName = "ntext")]
        public string TenNganh { get; set; }

        public double? SoTinChi { get; set; }

        public int? TrinhDo { get; set; }

        public int? SoThang { get; set; }

        public int? NamTS { get; set; }

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

        public int? He { get; set; }
    }
}
