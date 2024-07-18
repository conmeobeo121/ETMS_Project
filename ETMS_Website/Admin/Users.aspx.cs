using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            UsersBLL bLL = new UsersBLL();
            DataSet data = bLL.GetAllOfUsers();
            if (data.Tables[0].Rows.Count > 0)
            {
                gvUsers.DataSource = data;
                gvUsers.DataBind();
            }
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvUsers.Rows[e.RowIndex];
            HiddenField hdnIDUser = (HiddenField)row.FindControl("userID");
            UsersBLL bLL = new UsersBLL();
            bLL.DeleteUser(int.Parse(hdnIDUser.Value));
            gvUsers.EditIndex = -1;
            LoadData();
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            LoadData();

            GridViewRow row = gvUsers.Rows[e.NewEditIndex];
            DropDownList ddlUserTypes = (DropDownList)row.FindControl("ddlUserTypes");
            TextBox txtUsername = (TextBox)row.FindControl("txtUsername");
            UsersBLL bLL = new UsersBLL();
            ddlUserTypes.SelectedValue = bLL.GetRoleOfUser(txtUsername.Text);
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = gvUsers.Rows[e.RowIndex];
                HiddenField hdnIDUser = (HiddenField)row.FindControl("userID");
                TextBox txtUsername = (TextBox)row.FindControl("txtUsername");
                TextBox txtPassword = (TextBox)row.FindControl("txtPassword");
                TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
                string role = ((DropDownList)row.FindControl("ddlUserTypes")).SelectedValue;

                UsersBLL bLL = new UsersBLL();
                DataSet data = bLL.GetUserByUserName(txtUsername.Text);

                if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 && data.Tables[0].Rows[0]["Username"].ToString() != txtUsername.Text)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Username already exist. Please choose another username");
                    e.Cancel = true;
                    return;
                }

                bLL.UpdateUser(txtUsername.Text,
                    txtPassword.Text,
                    role,
                    txtEmail.Text,
                    int.Parse(hdnIDUser.Value));
                HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Updated user successfully.");

                gvUsers.EditIndex = -1;
                LoadData();
            }
            catch
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
            }
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadData();
        }

        private bool CheckUserValid(string username)
        {
            UsersBLL bLL = new UsersBLL();
            DataSet data = bLL.GetUserByUserName(username);
            return data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0;
        }

        private void ClearAllInputs()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            ddlUserTypes.SelectedValue = "Customer";
        }

        private bool RegisterUser(string username, string password, string role, string email)
        {
            try
            {
                ETMS_DatabaseHandle.BLL.UsersBLL bLL = new ETMS_DatabaseHandle.BLL.UsersBLL();
                bLL.CreateNewUser(username, password, role, email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string role = ddlUserTypes.SelectedValue;

            if (CheckUserValid(username))
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Username already exist. Please choose another username");
                return;
            }

            try
            {
                RegisterUser(username, password, role, email);
                ClearAllInputs();
                HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Added user successfully.");
                LoadData();
            }
            catch
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
            }
        }
    }
}