using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PagingParam
    {
        /// <summary>
        /// Trang hiện tại
        /// currentPage
        /// </summary>
        public int currentPage { get; set; } = 1;
        /// <summary>
        /// Số dòng trên một trang
        /// perPage
        /// </summary>
        public int perPage { get; set; } = 10;
    }
}
