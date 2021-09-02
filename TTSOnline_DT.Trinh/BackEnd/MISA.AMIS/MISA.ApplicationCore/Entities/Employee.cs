using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Employee : BaseEntity
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Khóa chính")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên 
        /// </summary>
        /// CreateBy: DT.Trinh
        [Duplicated]
        [Required]
        [MaxLength(20, "Mã nhân viên < 20 ký tự")]
        [DisplayName("Mã nhân viên ")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        /// CreateBy: DT.Trinh
        [Required]
        [DisplayName("Tên nhân viên ")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Giới tính")]

        public int? Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Tên giới tính")]
        public string GenderName
        {
            get
            {
                if (Gender == 0)
                {
                    return "Nữ";
                }
                else if (Gender == 1)
                {
                    return "Nam";
                }
                return "Khác";
            }
        }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        /// CreateBy: DT.Trinh
        [Required]
        [DisplayName("Id đơn vị")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Tên đơn vị")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Số CMND/căn cước công dân
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Số CMND/căn cước công dân")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Ngày cấp CMND")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Nơi cấp CMND")]
        public string IdentityPlace { get; set; }


        /// <summary>
        /// đơn vị làm việc
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("đơn vị")]
        public string EmployeePosition { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Tài khoản ngân hàng")]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Tên ngân hàng")]
        public string BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Chi nhánh ngân hàng")]
        public string BankBranchName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreateBy: DT.Trinh
        [FormatEmail]
        [DisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Số điện thoại di động")]
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreateBy: DT.Trinh
        [DisplayName("Số điện thoại cố định")]
        public string LandlineNumber { get; set; }

        #endregion
    }
}
