using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VMS.ESIApi
{
    public enum StatusCode
    {
        ///目前考虑3位状态码
        ///1xx:客户端请求码
        ///2xx:服务端响应
        ///5xx:服务端错误
        ///
        [Description("参数为空")]
        param_is_null=100,          //参数为空
        [Description("参数无效")]
        param_is_invalid = 101,     //参数无效
        [Description("参数类型错误")]
        param_Type_invalid =103,     //参数类型错误
        [Description("参数丢失")]
        PARAM_LOST =104,             //参数丢失

        [Description("响应成功")]
        SUCCESS =200,                //响应成功
        [Description("响应失败")]
        FAIL =201,                   //响应失败

        [Description("内存溢出")]
        out_of_memory,              //内存溢出
        [Description("接口废弃")]
        appid_lost                  //接口废弃
        
    }

}