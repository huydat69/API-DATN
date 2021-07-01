using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Service;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GraduateProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class LopHocApiController : ApiController
    {
        private LopHocService _lopHocService = new LopHocService();

        [HttpGet]
        [Route("api/lophoc/search")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Searchs([FromUri] PagingParam pagingParam, [FromUri] string maLop = null, [FromUri] string tenLop = null, [FromUri] string maPK = null)
        {
            return Ok(_lopHocService.Search(pagingParam, maLop, tenLop, maPK));
        }

        [HttpGet]
        [Route("api/lophoc/sinhvienlophoc/{maLop}")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult SinhvienLopHoc([FromUri] PagingParam pagingParam, string maLop)
        {
            return Ok(_lopHocService.SinhvienLopHoc(pagingParam, maLop));
        }

        [HttpGet]
        [Route("api/lophoc/detail/{maLop}")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Detail(string maLop)
        {
            return Ok(_lopHocService.Detail(maLop));
        }

        [HttpPost]
        [Route("api/lophoc/addsinhvienlophoc/{maLop}")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> AddSinhVienLopHoc(string maLop, [FromBody] JObject objMaSv)
        {
            string strMaSv = (string)objMaSv["strMaSv"];
            return Ok(await _lopHocService.AddSinhVienLopHoc(strMaSv, maLop));
        }
    }
}
