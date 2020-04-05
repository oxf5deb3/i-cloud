using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace VMS.ESIApi.Utils
{
    public class ESIExceptionTrace : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //var code = new HttpResponseMessage(HttpStatusCode.InternalServerError).StatusCode;//设置错误代码：例如：500 404
            //actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            //string msg = JsonConvert.SerializeObject(new BaseResult() { success = false, message = actionExecutedContext.Exception.Message });//返回异常错误提示
            //                                                                                                                                  //写入错误日志相关实现
            //portlogbll.SaveForm(new PortLogEntity()
            //{
            //    PortName = actionExecutedContext.Request.RequestUri.AbsolutePath,
            //    RequestType = actionExecutedContext.Request.Method.ToString(),
            //    StatusCode = Convert.ToInt32(code),
            //    ClientIp = GetClientIp(),
            //    ParameterList = actionExecutedContext.ActionContext.ActionArguments.ToJson(),
            //    Success = false,
            //    ErrorMessage = msg
            //});
            ////result
            //actionExecutedContext.Response.Content = new StringContent(msg, Encoding.UTF8);
        }
    }
}