namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSinhvien")]
    public partial class tblSinhvien
    {
        [Key]
        [StringLength(10)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(50)]
        public string HoVaTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        public int? GioiTinh { get; set; }

        [StringLength(50)]
        public string DanToc { get; set; }

        [StringLength(50)]
        public string SoDinhDanh { get; set; }

        [StringLength(50)]
        public string NoiCap { get; set; }

        public DateTime? NgayCap { get; set; }

        [StringLength(50)]
        public string DienThoai { get; set; }

        [Column(TypeName = "ntext")]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string MatKhau { get; set; }

        [StringLength(100)]
        public string QueQuan { get; set; }

        [StringLength(100)]
        public string NoiThuongTru { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhThe { get; set; }

        [Column(TypeName = "ntext")]
        public string AnhCMND { get; set; }

        public int? Quyen { get; set; }

        public int? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }
    }
}
