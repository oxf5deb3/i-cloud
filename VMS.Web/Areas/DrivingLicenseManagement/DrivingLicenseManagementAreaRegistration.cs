using System.Web.Mvc;

namespace VMS.Areas.DrivingLicenseManagement
{
    public class DrivingLicenseManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DrivingLicenseManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DrivingLicenseManagement_default",
                "DrivingLicenseManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}