using System.Web.Mvc;

namespace VMS.Areas.TrafficAccidentManagement
{
    public class TrafficAccidentManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TrafficAccidentManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TrafficAccidentManagement_default",
                "TrafficAccidentManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}