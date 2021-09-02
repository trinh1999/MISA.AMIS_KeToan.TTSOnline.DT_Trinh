using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class BaseEntity
    {
        #region Property
  
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Người tạo")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Người sửa")]
        public string ModifiedBy { get; set; }

        #endregion
    }
}
