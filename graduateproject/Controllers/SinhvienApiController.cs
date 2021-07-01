using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Common;
using Data;
using ExcelDataReader;
using Service;
namespace GraduateProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class SinhvienApiController : ApiController
    {
        private SinhvienService _sinhvienService = new SinhvienService();
        [HttpGet]
        [Route("api/sinhvien/search")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Search([FromUri] PagingParam pagingParam, [FromUri] string masv = null, [FromUri] string hovaten = null, [FromUri] string malop = null)
        {
            return Ok(_sinhvienService.Search(pagingParam, masv, hovaten, malop));
        }

        [HttpGet]
        [Route("api/sinhvien/infomation")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Infomation([FromUri] string masv = null)
        {
            return Ok(_sinhvienService.Infomation(masv));
        }

        [HttpGet]
        [Route("api/sinhvien/detail")]
        [ResponseType(typeof(ApiResult))]
        public IHttpActionResult Detail([FromUri] string masv = null)
        {
            return Ok(_sinhvienService.Detail(masv));
        }

        [HttpPost]
        [Route("api/sinhvien/add")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> Add(tblSinhvien sv)
        {
            return Ok(await _sinhvienService.Add(sv));
        }

        [HttpPut]
        [Route("api/sinhvien/update")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> Update(tblSinhvien sv)
        {
            return Ok(await _sinhvienService.Update(sv));
        }

        [HttpDelete]
        [Route("api/sinhvien/delete")]
        [ResponseType(typeof(ApiResult))]
        public async Task<IHttpActionResult> Delete(string masv)
        {
            return Ok(await _sinhvienService.Delete(masv));
        }

        [Route("api/sinhvien/importfile")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportFile()
        {
            try
            {
                #region Variable Declaration  
                string message = "";
                HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Student Detail From Excel  
                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            message = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtStudentRecords = dsexcelRecords.Tables[0];

                            var result = await _sinhvienService.ImportFile(dtStudentRecords);
                            return Ok(result);

                        }
                        else
                            message = "Selected file is empty.";
                    }
                    else
                        message = "Invalid File.";
                }
                else
                    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                return Ok(message);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
