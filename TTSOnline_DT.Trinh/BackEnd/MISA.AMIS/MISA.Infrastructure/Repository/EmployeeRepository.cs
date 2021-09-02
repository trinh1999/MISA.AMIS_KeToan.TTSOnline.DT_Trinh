using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructor.Repository
{

    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        #region Contructor

        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion


        #region Method
        /// <summary>
        /// Tạo mã mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// CreateBy: DT.Trinh
        public string GetNewEmployeeCode()
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                string sqlCommand = "SELECT EmployeeCode FROM Employee ORDER BY  LENGTH(EmployeeCode) DESC, EmployeeCode DESC LIMIT 1";
                var employeeCode = _dbConnection.QueryFirstOrDefault<string>(sqlCommand);
                return employeeCode;                                                                  
            }

        }

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>
        /// <returns>Danh sách khách hàng theo điều kiện lọc</returns>
        /// Created By : DT.Trinh
        public Paging<Employee> GetEmployeesByPaging(EmployeeFilter filter)
        {
            using (_dbConnection = new MySqlConnection(_connectionString))
            {
                Paging<Employee> pageNew = new Paging<Employee>();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pageIndex", filter.PageIndex);
                parameters.Add("@pageSize", filter.PageSize);
                parameters.Add("@keySearch", filter.KeySearch);

                parameters.Add("@totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@totalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
              
                var sqlCommand = "Proc_GetEmployeesByPaging";
                var employees = _dbConnection.Query<Employee>(sqlCommand, param: parameters, commandType: CommandType.StoredProcedure);

                var totalPage = parameters.Get<int>("@totalPage");
                var totalRecord = parameters.Get<int>("@totalRecord");

                pageNew = new Paging<Employee>
                {
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                    PageIndex = filter.PageIndex,
                    Data = employees,
                    PageSize = filter.PageSize
                };
                return pageNew;
            }
            #endregion
        }
    }
}
