using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OnLineExam.CommonComponent
{
    /// <summary>
    /// 用户类型 枚举  学生，教师，管理员
    /// </summary>
    public enum UserType
    {
        [EnumDescription("Student")]
        Student,
        [EnumDescription("Teacher")]
        Teacher,
        [EnumDescription("Manager")]
        Manager
    }


    /*  为enum 服务 返回 string         */
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description { get { return description; } }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }

    public static class EnumHelper
    {
        public static string GetDescription(UserType value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes =
                (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }




    //下面打印结果为: 星期一  
    //Console.WriteLine(EnuHelper.GetDescription(Week.Monday))  
    /*  为enum 服务 返回 string         */


}