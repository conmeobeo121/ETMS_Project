using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web;
using System.Web.Security;

namespace ETMS_Website
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ClearAllInputs()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtRePassword.Text = "";
            ckbAutoLogin.Checked = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            bool autoLogin = ckbAutoLogin.Checked;

            if (CheckUserValid(username))
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Username already exist. Please choose another username");
                return;
            }

            if (RegisterUser(username, password, email))
            {
                HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Registered successfully.");
                if (autoLogin)
                {
                    AutoLogin(username, password);
                }
                else
                {
                    ClearAllInputs();
                }
            }
            else
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Registration failed. Please try again.");
            }
        }

        private bool CheckUserValid(string username)
        {
            UsersBLL bLL = new UsersBLL();
            DataSet data = bLL.GetUserByUserName(username);
            return data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0;
        }

        private bool RegisterUser(string username, string password, string email)
        {
            try
            {
                ETMS_DatabaseHandle.BLL.UsersBLL bLL = new ETMS_DatabaseHandle.BLL.UsersBLL();
                bLL.CreateNewUser(username, password, "Customer", email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AutoLogin(string username, string password)
        {
            if (ValidateUser(username, password))
            {
                string role = GetRolesForUser(username);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1, username, DateTime.Now, DateTime.Now.AddMinutes(60), false, role, FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                Response.Redirect(FormsAuthentication.GetRedirectUrl(username, false));
            }
            else
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Auto login failed.");
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