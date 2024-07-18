using System;

namespace ETMS_Website
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Admin"))
                {
                    Response.Redirect("~/Admin");
                }
                else
                {
                    Response.Redirect("~/User");
                }
            }
        }
    }
}