namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDiemHocPhanChiTiet")]
    public partial class tblDiemHocPhanChiTiet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ThuTu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaSV { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string MaHP { get; set; }

        public double? Diem { get; set; }

        public int? HocKy { get; set; }

        public int? NamHoc { get; set; }

        [Column(TypeName = "text")]
        public string DiemThanhPhan { get; set; }

        public bool? TinhTrang { get; set; }

        [StringLength(10)]
        public string NguoiDay { get; set; }

        [Column(TypeName = "ntext")]
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
    }
}
