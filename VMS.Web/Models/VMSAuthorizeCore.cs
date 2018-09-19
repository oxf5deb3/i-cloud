using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VMS.Controllers;
using VMS.DTO;

namespace VMS.Models
{
    public class VMSAuthorizeCore : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            LoginDTO info = GlobalVar.get();

            bool Pass = false;
            if (info == null)
            {
                //httpContext.Response.StatusCode = 401;
                Pass = false;
            }
            else
            {
                Pass = true;
            }
            return Pass;
        
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.HttpContext.Response.Write(filterContext.HttpContext.Response.StatusCode);
            string path = filterContext.HttpContext.Request.Path;
            string strUrl = "/Login/Index";
            //filterContext.Result = new EmptyResult();
            //filterContext.HttpContext.Response.RedirectLocation = strUrl;
           
            //filterContext.HttpContext.Response.RedirectToRoute(new RouteValueDictionary(new { controller = "Account", action = "Index" }));
            filterContext.HttpContext.Response.Redirect(strUrl);
            //if (filterContext.HttpContext.Response.StatusCode == 401)
            //{
               
               // filterContext.Result = new RedirectResult("/Login");
               
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "hello" }));
            //}
        }
    }
}