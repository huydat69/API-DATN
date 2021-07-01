using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Service;

namespace GraduateProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class ClientApiController : ApiController
    {
        private ClientService _clientService = new ClientService();
        private string UserID { get; set; }
        public ClientApiController()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userID = identity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            this.UserID = Convert.ToString(userID);
        }

        [HttpGet]
        [Route("api/client/tracuudiem")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult TraCuuDiem([FromUri] string maSV = null, [FromUri] string maLop = null, [FromUri] string maHP = null)
        {
            return Ok(_clientService.TraCuuDiem(maSV, maLop, maHP));
        }

        [HttpGet]
        [Route("api/client/tracuukhoanthu")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult TraCuuKhoanThu([FromUri] string maKhoanThu = null)
        {
            return Ok(_clientService.TraCuuKhoanThu(maKhoanThu));
        }

        [HttpGet]
        [Route("api/client/thongtincanhansinhvien")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult ThongTinCaNhanSinhVien()
        {
            return Ok(_clientService.ThongTinCaNhanSinhVien(this.UserID));
        }

        [HttpGet]
        [Route("api/client/diemthisinhvien")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult DiemThiSinhVien([FromUri] PagingParam pagingParam, [FromUri] int? hocky = null, [FromUri] int? namhoc = null)
        {
            return Ok(_clientService.DiemThiSinhVien(pagingParam, this.UserID, hocky, namhoc));
        }

        [HttpGet]
        [Route("api/client/khoannopsinhvien")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult KhoanThuSinhVien()
        {
            return Ok(_clientService.KhoanThuSinhVien(this.UserID));
        }

        [HttpGet]
        [Route("api/client/Taokhoanthusinhvien")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> TaoKhoanThuSinhVien([FromUri] string maLop = null, [FromUri] string MaKhoanThuStr = null)
        {
            return Ok(await _clientService.TaoKhoanThuSinhVien(maLop, MaKhoanThuStr));
        }

        [HttpGet]
        [Route("api/client/Taophieuthusinhvien")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> TaoPhieuThuSinhVien([FromUri] string maLop = null)
        {
            return Ok(await _clientService.TaoPhieuThuSinhVien(maLop));
        }
    }
}
