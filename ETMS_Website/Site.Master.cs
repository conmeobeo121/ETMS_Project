using System;
using System.Web.UI;

namespace ETMS_Website
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            titleMaster.Text = Page.Title + " - Event ticket System";
            //if (!IsPostBack)
            //{
            //    if (Context.User.Identity.IsAuthenticated && Context.User.IsInRole("Admin"))
            //    {
            //        Response.Redirect("~/Admin");
            //    }
            //    else
            //    {
            //        Response.Redirect("~/User");
            //    }
            //}
        }
    }
}