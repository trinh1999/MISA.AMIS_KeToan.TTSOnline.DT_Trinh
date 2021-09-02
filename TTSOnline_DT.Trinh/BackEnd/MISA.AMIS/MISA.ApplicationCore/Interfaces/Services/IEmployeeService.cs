using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Services
{
    public interface IEmployeeService: IBaseService<Employee>
    {
 
        #region METHOD
        /// <summary>
        /// Tạo mã mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        ///CreateBy: DT.Trinh
        string GetNewEmployeeCode();

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>   
        /// <returns>Danh sách khách hàng theo đk</returns>
        /// Created By : DT.Trinh
        public Paging<Employee> GetEmployeesByPaging(EmployeeFilter filter);
        #endregion

    }
}
