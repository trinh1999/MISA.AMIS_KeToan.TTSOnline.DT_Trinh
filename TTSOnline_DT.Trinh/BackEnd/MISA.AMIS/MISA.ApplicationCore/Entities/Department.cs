using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Department : BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Khóa chính")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Mã đơn vị")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Tên đơn vị")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        #endregion
    }

}
