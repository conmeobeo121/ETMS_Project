using System;

namespace ETMS_Website
{
    public partial class Navbar_User : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetNameByUsername()
        {
            return Context.User.Identity.Name; // Default: username, will be update to get name by username in the future.
        }
    }
}