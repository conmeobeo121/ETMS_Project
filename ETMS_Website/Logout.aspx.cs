using System;
using System.Web.Security;

namespace ETMS_Website
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogoutUser();
            }
        }

        private void LogoutUser()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/", false);
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}