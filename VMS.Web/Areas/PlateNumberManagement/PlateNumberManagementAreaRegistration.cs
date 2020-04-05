using System.Web.Mvc;

namespace VMS.Areas.PlateNumberManagement
{
    public class PlateNumberManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PlateNumberManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PlateNumberManagement_default",
                "PlateNumberManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}