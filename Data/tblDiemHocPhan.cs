namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDiemHocPhan")]
    public partial class tblDiemHocPhan
    {
        [Key]
        public long MaDiem { get; set; }

        [StringLength(10)]
        public string MaSV { get; set; }

        [StringLength(10)]
        public string MaHP { get; set; }

        public long? MaKH { get; set; }

        public double? Diem { get; set; }

        public int? HocKy { get; set; }

        public int? NamHoc { get; set; }

        public bool? TinhTrang { get; set; }

        public int? SoLanHoc { get; set; }

        public double? SoTinChi { get; set; }

        public double? HeSo { get; set; }

        [Column(TypeName = "ntext")]
        public string DiemThanhPhan { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
    }
}
