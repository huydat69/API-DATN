namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblKhoanThu")]
    public partial class tblKhoanThu
    {
        [Key]
        [StringLength(10)]
        public string MaKhoanThu { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public decimal? SoTien { get; set; }

        public int? NamHocTu { get; set; }

        public int? NamHocDen { get; set; }

        public int? HocKy { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
    }
}
