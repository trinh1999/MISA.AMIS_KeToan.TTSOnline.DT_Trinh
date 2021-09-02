﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.MISAAttribute
{
    /// <summary>
    /// Thuộc tính để check bắt buộc nhập
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    /// <summary>
    /// Thuộc tính dùng để check trùng mã
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class Duplicated : Attribute
    {

    }


    /// <summary>
    /// Thuộc tính để check định dạng
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class Format: Attribute
    {

    }
    /// <summary>
    /// Thuộc tính dùng để check độ dài
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int Length { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int length, string errorMsg)
        {
            Length = length;
            ErrorMsg = errorMsg;
        }
    }

    /// <summary>
    /// Thuộc tính dùng để hiển thị tên gọi
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Check format email
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatEmail: Attribute
    {
        public FormatEmail()
        {

        }
    }

    /// <summary>
    /// Check format của cmt/ căn cước
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatIdentityNumber : Attribute
    {
        public FormatIdentityNumber()
        {

        }
    }

    /// <summary>
    /// Check format số điện thoại
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatPhoneNumber : Attribute
    {
        public FormatPhoneNumber()
        {

        }
    }

    /// <summary>
    /// Không lưu vào csdl
    /// </summary>
    /// CreatedBy: DT.Trinh
    [AttributeUsage(AttributeTargets.Property)]
    public class MISANotMap : Attribute
    {
        public MISANotMap()
        {

        }
    }
}
