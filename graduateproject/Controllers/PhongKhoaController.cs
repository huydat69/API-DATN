using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Service;

namespace GraduateProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class PhongKhoaController : ApiController
    {
        private PhongKhoaService _phongKhoaService = new PhongKhoaService();

        [HttpGet]
        [Route("api/phongkhoa/search")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Searchs([FromUri] PagingParam pagingParam, [FromUri] string maPK = null, [FromUri] string tenPhongKhoa = null)
        {
            return Ok(_phongKhoaService.Search(pagingParam, maPK, tenPhongKhoa));
        }

        [HttpGet]
        [Route("api/phongkhoa/detail/{maPK}")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Detail(string maPK)
        {
            return Ok(_phongKhoaService.Detail(maPK));
        }
    }
}
