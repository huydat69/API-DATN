using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Data;
using Newtonsoft.Json.Linq;
using Service;

namespace GraduateProject.Controllers
{
    public class DiemHocPhanController : ApiController
    {
        private DiemHocPhanService _diemHocPhanService = new DiemHocPhanService();

        [HttpGet]
        [Route("api/diemhocphan/search")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Search([FromUri] PagingParam pagingParam, [FromUri] string maSv = null, [FromUri] string maPK = null, [FromUri] string maLop = null, [FromUri] string maHP = null)
        {
            return Ok(_diemHocPhanService.Search(pagingParam, maSv, maPK, maLop, maHP));
        }

        [HttpGet]
        [Route("api/diemhocphan/diemhocphanchitiet")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult DiemHocPhanChiTiet([FromUri] PagingParam pagingParam, [FromUri] string maSv = null, [FromUri] string maHP = null)
        {
            return Ok(_diemHocPhanService.DiemHocPhanChiTiet(pagingParam, maSv, maHP));
        }

        [HttpGet]
        [Route("api/diemhocphan/SearhSinhVienHocLai")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult SearhSinhVienHocLai([FromUri] PagingParam pagingParam, [FromUri] string maSv = null, [FromUri] string maLop = null, [FromUri] string maNganh = null, [FromUri] int? hocKy = null, [FromUri] int? namHoc = null)
        {
            return Ok(_diemHocPhanService.SearhSinhVienHocLai(pagingParam, maSv, maLop, maNganh, hocKy, namHoc));
        }

        [HttpGet]
        [Route("api/diemhocphan/kehoachgiangday")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult KeHoachGiangDay()
        {
            return Ok(_diemHocPhanService.KeHoachGiangDay());
        }

        [HttpGet]
        [Route("api/diemhocphan/detail/{maDiem}")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Detail(long maDiem)
        {
            return Ok(_diemHocPhanService.Detail(maDiem));
        }

        [HttpPost]
        [Route("api/diemhocphan/add")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> Add(Data.Dto.DiemHocPhan dhp)
        {
            return Ok(await _diemHocPhanService.Add(dhp));
        }

        [HttpPost]
        [Route("api/diemhocphan/updatediemhoclai")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> UpdateDiemHocLai(Data.Dto.DiemHocPhan dhp)
        {
            return Ok(await _diemHocPhanService.UpdateDiemHocLai(dhp));
        }

        [HttpPut]
        [Route("api/diemhocphan/update/{maDiem}")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> Update(long maDiem, JObject data)
        {
            double diem = (double)data["Diem"];
            string ghichu = (string)data["GhiChu"];

            return Ok(await _diemHocPhanService.Update(maDiem, diem, ghichu));
        }

        [HttpDelete]
        [Route("api/diemhocphan/delete/{maDiem}")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> Delete(long maDiem)
        {
            return Ok(await _diemHocPhanService.Delete(maDiem));
        }
    }
}
