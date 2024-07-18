using System;
using System.Web;
using System.Web.Security;

namespace ETMS_Website
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            bool rememberMe = ckbRememberMe.Checked;

            if (ValidateUser(username, password))
            {
                string role = GetRolesForUser(username);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(60),
                        rememberMe,
                        role,
                        FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                if (rememberMe)
                {
                    authCookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(authCookie);
                string previousPageUrl = Request.QueryString["returnUrl"];
                if (!String.IsNullOrEmpty(previousPageUrl))
                {
                    Response.Redirect(previousPageUrl);
                }
                else
                {
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(username, rememberMe));
                }
            }
            else
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Invalid username or password.");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            ETMS_DatabaseHandle.BLL.UsersBLL bLL = new ETMS_DatabaseHandle.BLL.UsersBLL();
            return bLL.CheckLoginValid(username, password);
        }

        private string GetRolesForUser(string username)
        {
            ETMS_DatabaseHandle.BLL.UsersBLL bLL = new ETMS_DatabaseHandle.BLL.UsersBLL();
            return bLL.GetRoleOfUser(username);
        }
    }
}