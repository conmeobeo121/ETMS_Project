using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS_Website
{
    public static class HandleFunction
    {
        public static void SetLbMessage(Label label, string message, string cssClass)
        {
            label.Text = message;
            label.CssClass = cssClass;
        }

        public static void GoToErrorPage(HttpResponse response, HttpContext context, string message)
        {
            response.Redirect($"~/ErrorPage.aspx?Error={message}", false);
            context.ApplicationInstance.CompleteRequest();
        }

        public static void SetupToastr(Control control, Type type, string typeToastr, string header, string message)
        {
            string script = $"toastr.{typeToastr.ToLower()}('{message}', '{header}');";
            ScriptManager.RegisterStartupScript(control, type, "OpenToastr", script, true);
        }
    }
}