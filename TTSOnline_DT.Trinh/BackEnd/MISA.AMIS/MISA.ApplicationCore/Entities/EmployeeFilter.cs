using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class EmployeeFilter
    {
        /// <summary>
        /// Trang hiện tại
        /// Created By : DT.Trinh
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Số lượng bản ghi có trong 1 trang
        /// Created By : DT.Trinh
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Lọc theo Mã NV / SĐT / HọTen
        /// Created By : DT.Trinh
        /// </summary>
        public string KeySearch { get; set; } = "";

       
    }
}
