namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietKhoanThu")]
    public partial class ChiTietKhoanThu
    {
        [Key]
        public long MaChiTietKhoanThu { get; set; }

        [StringLength(10)]
        public string MaKhoanThu { get; set; }

        [StringLength(200)]
        public string LoaiThu { get; set; }

        public decimal? SoTienNop { get; set; }

        public decimal? SoTienMienGiam { get; set; }

        public decimal? SoTienPhaiNop { get; set; }

        public decimal? SoTienDaNop { get; set; }

        public decimal? ThuaThieu { get; set; }

        public bool? DaNop { get; set; }
    }
}
