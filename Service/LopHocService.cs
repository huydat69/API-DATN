using Common;
using Data.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace Service
{
    public class LopHocService
    {
        private LophocRepository _lophocRepository = new LophocRepository();

        public ApiResult Search(PagingParam pagingParam, string maLop, string tenLop, string maPK)
        {
            try
            {
                object result = _lophocRepository.Search(pagingParam, maLop, tenLop, maPK);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult SinhvienLopHoc(PagingParam pagingParam, string maLop)
        {
            try
            {
                object result = _lophocRepository.SinhvienLopHoc(pagingParam, maLop);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult Detail(string maLop)
        {
            try
            {
                object result = _lophocRepository.Detail(maLop);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }

        public async Task<ApiResult> AddSinhVienLopHoc(string strMaSv, string maLop)
        {
            if (string.IsNullOrEmpty(strMaSv))
                return new ApiResult() { success = false, message = "", data = null };

            string[] maSvArray = strMaSv.Split(',');

            if (maSvArray.Length <= 0)
                return new ApiResult() { success = false, message = "", data = null };

            try
            {
                string[] svSuccess = { };
                foreach (string maSv in maSvArray)
                {
                    bool sv = await _lophocRepository.AddSinhVienLopHoc(maSv.Trim(), maLop);
                    if (sv)
                        svSuccess = svSuccess.Concat(new string[] { maSv.Trim() }).ToArray();
                }

                return new ApiResult() { success = svSuccess.Any() ? true : false, message = "", data = svSuccess };
            }
            catch { throw; }
        }
    }
}
