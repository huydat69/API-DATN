using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Data;
using Newtonsoft.Json.Linq;
using Service;

namespace GraduateProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class PhieuThuController : ApiController
    {
        private PhieuThuService _phieuthuService = new PhieuThuService();

        [HttpGet]
        [Route("api/phieuthu/danhsachphieuthu")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult DanhSachPhieuThu([FromUri] PagingParam pagingParam, [FromUri] string maSv = null, [FromUri] string soPhieu = null)
        {
            return Ok(_phieuthuService.DanhSachPhieuThu(pagingParam, maSv, soPhieu));
        }

        [HttpGet]
        [Route("api/phieuthu/chitietphieuthu/{maPhieuThu}")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult ChiTietPhieuThu(int maPhieuThu, [FromUri] PagingParam pagingParam)
        {
            return Ok(_phieuthuService.ChiTietPhieuThu(pagingParam, maPhieuThu));
        }
    }
}