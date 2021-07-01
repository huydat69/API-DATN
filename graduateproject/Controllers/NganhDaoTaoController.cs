using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Service;

namespace GraduateProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class NganhDaoTaoController : ApiController
    {
        private NganhDaoTaoService _nganhDaoTaoService = new NganhDaoTaoService();

        [HttpGet]
        [Route("api/nganhdaotao/search")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Searchs([FromUri] PagingParam pagingParam, [FromUri] string maNganh = null, [FromUri] string tenNganh = null)
        {
            return Ok(_nganhDaoTaoService.Search(pagingParam, maNganh, tenNganh));
        }

        [HttpGet]
        [Route("api/nganhdaotao/detail/{maNganh}")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Detail(string maNganh)
        {
            return Ok(_nganhDaoTaoService.Detail(maNganh));
        }
    }
}
