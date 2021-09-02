using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Repository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
       
        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
         string GetNewEmployeeCode();

        /// <summary>
        ///  Tìm kiếm 
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>
        /// <returns>Danh sách khách hàng theo điều kiện lọc</returns>
        /// Created By : DT.Trinh
        public Paging<Employee> GetEmployeesByPaging(EmployeeFilter filter);


    }
}
