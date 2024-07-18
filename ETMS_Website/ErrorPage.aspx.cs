using System;

namespace ETMS_Website
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string errorMessage = Request.QueryString["Error"];
                if (errorMessage != null)
                {
                    lbMessage.Text = errorMessage;
                }
            }
        }
    }
}