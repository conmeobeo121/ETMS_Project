using System;
using System.Collections.Generic;
using System.Web;

namespace ETMS_Website.App_Code
{
    public class MiddlewareAdminModule : IHttpModule
    {
        private List<string> routesToCheck = new List<string>()
        {
            "~/Admin/"
        };

        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += OnAuthorizeRequestAdmin;
        }

        private void OnAuthorizeRequestAdmin(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;


            foreach (var route in routesToCheck)
            {
                if (context.Request.AppRelativeCurrentExecutionFilePath.StartsWith(route))
                {
                    Checking(context);
                    break;
                }
            }
        }

        private void Checking(HttpContext context)
        {
            if (context.User == null || context.User.Identity == null)
            {
                HandleFunction.GoToErrorPage(context.Response, context, "Invalid page.");
                return;
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("~/Login.aspx?returnUrl=" + context.Server.UrlEncode(context.Request.Url.AbsoluteUri), false);
                context.ApplicationInstance.CompleteRequest();
                return;
            }
            if (!context.User.IsInRole("Admin"))
            {
                HandleFunction.GoToErrorPage(context.Response, context, "You do not have permission to access this page.");
                return;
            }
        }

        public void Dispose() { }
    }
}