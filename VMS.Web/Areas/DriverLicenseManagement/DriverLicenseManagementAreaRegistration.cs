using System.Web.Mvc;

namespace VMS.Areas.DriverLicenseManagement
{
    public class DriverLicenseManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DriverLicenseManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DriverLicenseManagement_default",
                "DriverLicenseManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}