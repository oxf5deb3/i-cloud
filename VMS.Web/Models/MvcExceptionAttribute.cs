using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Models
{
    public class MvcExceptionAttribute: HandleErrorAttribute
    {
        public static Queue<Exception> exceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            
            Exception ex = filterContext.Exception;
            //写到队列
            exceptionQueue.Enqueue(ex);
            //跳转到错误页面
            //Redirect or return a view, but not both.

            //filterContext.HttpContext.Response.Redirect("~/Error.cshtml");
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "~/Views/Error.cshtml",
            //};
            //filterContext.HttpContext.Response.Redirect("~/ErrorHandler/Index");
            base.OnException(filterContext);
        }
    }

}