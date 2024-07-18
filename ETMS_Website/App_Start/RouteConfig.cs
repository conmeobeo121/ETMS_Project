using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace ETMS_Website
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            //routes.MapPageRoute("CustomPageRouteTest", "", "~/MyPageForTest.aspx");
            //routes.MapPageRoute("CustomPageRouteTest1", "Default", "~/MyPageForTest.aspx");
            //routes.MapPageRoute("CustomPageRouteTest2", "Login", "~/MyPageForTest.aspx");
        }
    }
}
