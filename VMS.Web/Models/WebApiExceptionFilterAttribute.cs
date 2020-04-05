using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace VMS.Models
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            MvcExceptionAttribute.exceptionQueue.Enqueue(actionExecutedContext.Exception);

            //自定义异常的处理
            if (actionExecutedContext.Exception is NotImplementedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent("{\"Code\":501,\"ErrorMsg\":" + actionExecutedContext.Exception.Message + "\"}", Encoding.UTF8, "application/json"),
                    ReasonPhrase = "NotImplementedException"
                });
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent("{\"Code\":408,\"ErrorMsg\":" + actionExecutedContext.Exception.Message + "\"}", Encoding.UTF8, "application/json"),
                    ReasonPhrase = "TimeoutException"
                });
            }
            //.....这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = new StringContent("{\"Code\":500,\"ErrorMsg\":"+ actionExecutedContext.Exception.Message+ "\"}", Encoding.UTF8, "application/json"),
                    ReasonPhrase = "InternalServerErrorException"
                });
            }

            //base.OnException(context);

            //记录关键的异常信息
            //Debug.WriteLine(context.Exception);
        }
    }
}