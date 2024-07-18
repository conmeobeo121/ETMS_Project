using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ETMS_Website.Admin
{
    public partial class NavbarAdmin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HighlightCurrentPage();
            }
        }

        private void HighlightCurrentPage()
        {
            string currentPage = System.IO.Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Url.AbsolutePath).ToLower();

            foreach (Control control in MenuItems.Controls)
            {
                if (control is HtmlAnchor anchor)
                {
                    string hrefAnchor = System.IO.Path.GetFileNameWithoutExtension(anchor.HRef).ToLower();
                    if (hrefAnchor.Equals(currentPage))
                    {
                        anchor.Attributes["class"] += " active";
                    }
                }
            }
        }
    }
}