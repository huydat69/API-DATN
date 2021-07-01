using Common;
using Data.Repositories;

namespace Service
{
    public class NganhDaoTaoService
    {
        private NganhDaoTaoRepository _nganhDaoTaoRepository = new NganhDaoTaoRepository();

        public ApiResult Search(PagingParam pagingParam, string maNganh, string tenNganh)
        {
            try
            {
                object result = _nganhDaoTaoRepository.Search(pagingParam, maNganh, tenNganh);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }
            catch { throw; }
        }

        public ApiResult Detail(string maNganh)
        {
            try
            {
                object result = _nganhDaoTaoRepository.Detail(maNganh);
                return new ApiResult() { success = true, message = string.Empty, data = result };
            }

            catch { throw; }
        }
    }
}
