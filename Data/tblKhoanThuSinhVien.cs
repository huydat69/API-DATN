namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblKhoanThuSinhVien")]
    public class tblKhoanThuSinhVien
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaKhoanThu { get; set; }
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaSV { get; set; }
        public bool? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}
