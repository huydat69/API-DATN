namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblKeHoachGiangDay")]
    public partial class tblKeHoachGiangDay
    {
        [Key]
        public long maKH { get; set; }

        [StringLength(50)]
        public string TenKeHoach { get; set; }

        [StringLength(10)]
        public string MaCBGV { get; set; }

        [StringLength(10)]
        public string MaHP { get; set; }

        public int? Hocky { get; set; }

        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(50)]
        public string NguoiTao { get; set; }

        public bool? trangthai { get; set; }
    }
}
