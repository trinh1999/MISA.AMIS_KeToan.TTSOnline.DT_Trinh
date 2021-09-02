﻿using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using MISA.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE

        private IEmployeeRepository _employeeRepository;
        ServiceResult _serviceResult;

        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo employeeservice
        /// </summary>
        /// <param name="employeeRepository"></param>
        /// CreatedBy: DT.Trinh
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _serviceResult = new ServiceResult();
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm validate custom cho nhan viên
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        /// CreateBy: DT.Trinh
        protected override bool ValidateCustom(Employee entity)
        {
            var propName = "EmployeeCode";
            var codeFormat = @"^NV-[0-9]*$";
            var propValue = entity.GetType().GetProperty(propName).GetValue(entity);
            var isMatchCode = Regex.IsMatch(propValue.ToString(), codeFormat, RegexOptions.IgnoreCase);
            return isMatchCode;
        }

        /// <summary>
        /// Hàm thêm mới nhân viên
        /// </summary>
        /// <param name="entity">Object nhân viên</param>
        /// <returns>ServiceResult</returns>
        /// CreateBy: DT.Trinh
        public override ServiceResult Add(Employee entity)
        {
            var isValid = ValidateCustom(entity);
            if (!isValid)
            {
                var msgError = new
                {
                    devMsg = string.Format(Properties.ResourcesVN.ErrorDevMsgValidate, "Mã Nhân viên"),
                    userMs = string.Format(Properties.ResourcesVN.ErrorUserMsgValidate, "Mã Nhân viên"),
                    dataError = "EmployeeCode",
                };
                _serviceResult.Messager = string.Format(Properties.ResourcesVN.ErrorDevMsgValidate, "Mã Nhân viên");
                _serviceResult.Data = msgError;
                _serviceResult.ErrorCode = MISACode.NoValid;
                return _serviceResult;
            }
            return base.Add(entity);
        }

        /// <summary>
        /// Sửa nhân viên
        /// </summary>
        /// <param name="entity">Object cập nhật</param>
        /// <param name="entityId">id</param>
        /// <returns>Kết quả ServiceResult</returns>
        /// CreateBy: DT.Trinh
        public override ServiceResult Update(Employee entity, Guid entityId)
        {
            var isValid = ValidateCustom(entity);
            if (!isValid)
            {
                var msgError = new
                {
                    devMsg = string.Format(Properties.ResourcesVN.ErrorDevMsgValidate, "Mã Nhân viên"),
                    userMs = string.Format(Properties.ResourcesVN.ErrorUserMsgValidate, "Mã Nhân viên"),
                };
                _serviceResult.Messager = string.Format(Properties.ResourcesVN.ErrorDevMsgValidate, "Mã Nhân viên");
                _serviceResult.Data = msgError;
                _serviceResult.ErrorCode = MISACode.NoValid;
                return _serviceResult;
            }
            return base.Update(entity, entityId);
        }

        /// <summary>
        /// Tạo mã nhân viên mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// CreateBy: DT.Trinh
        public string GetNewEmployeeCode()
        {
            var employeeCode = _employeeRepository.GetNewEmployeeCode();
            int currentMax = 0;
            int codeValue = 0;
            if (employeeCode.Contains("-"))
            {
                codeValue = int.Parse(employeeCode.ToString().Split("-")[1]);
            }
            else
            {
                codeValue = int.Parse(employeeCode.ToString().Split("NV-")[1]);
            }

            if (currentMax < codeValue)
            {
                currentMax = codeValue;
            }
            string newEmployeeCode = "NV-" + (currentMax + 1);
            return newEmployeeCode;
        }



        #endregion



        /// <summary>
        ///  Lấy danh sách khách hàng theo điều kiện lọc
        /// </summary>
        /// <param name="filter">Object chứa nội dung lọc</param>
        /// <returns>Danh sách khách hàng theo điều kiện lọc</returns>
        /// Created By : DT.Trinh
        public Paging<Employee> GetEmployeesByPaging(EmployeeFilter filter)
        {
            return _employeeRepository.GetEmployeesByPaging(filter);
        }

    }


}
