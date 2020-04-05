using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace VMS.ESIApi.Utils
{
    public class ESIAopFilter: ActionFilterAttribute
    {
        //主要统计以下信息
        //1.接口执行时间
        //2.接口访问次数
      
        //before
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }
        //after
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}