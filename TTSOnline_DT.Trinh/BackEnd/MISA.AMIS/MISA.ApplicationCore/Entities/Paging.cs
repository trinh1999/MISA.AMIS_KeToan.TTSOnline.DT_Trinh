using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Paging<TEnity> where TEnity : class
    {
        /// <summary>
        /// Tổng số bản ghi
        /// Created By : DT.Trinh
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Tổng số trang
        /// Created By : DT.Trinh
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Dữ liệu phân trang
        /// Created By : DT.Trinh
        /// </summary>
        public IEnumerable<TEnity> Data { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// Created By : DT.Trinh
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Số lượng bản ghi có trong 1 trang
        /// Created By : DT.Trinh 
        /// </summary>
        public int PageSize { get; set; }
    }
}
