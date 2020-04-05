using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace VMS.ESIApi.Utils
{
    public static class EnumUtil
    {
        #region FetchDescription 
        /// <summary> 
        /// 获取枚举值的描述文本 
        /// </summary> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public static string FetchDescription(this Enum value,string defaultDescription="")
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
               (DescriptionAttribute[])fi.GetCustomAttributes(
               typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : defaultDescription;
        }
        #endregion
    }
}