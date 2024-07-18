using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ETMS_Website
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string[] roles = { ticket.UserData };
                        var principal = new System.Security.Principal.GenericPrincipal(id, roles);
                        principal.AddIdentity(id);
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                    }
                }
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            if (Session["cartData"] == null || !(Session["cartData"] is Dictionary<int, int>))
            {
                Session["cartData"] = new Dictionary<int, int>();
            }
        }
    }
}