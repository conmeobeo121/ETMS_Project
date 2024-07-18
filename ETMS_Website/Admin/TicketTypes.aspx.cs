using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin
{
    public partial class TicketTypes : System.Web.UI.Page
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
            TicketTypesBLL bLL = new TicketTypesBLL();
            DataSet data = bLL.GetAllTicketTypesWithEventName();
            if (data.Tables[0].Rows.Count > 0)
            {
                gvTicketTypes.DataSource = data;
                gvTicketTypes.DataBind();
            }
        }

        protected void btnAddTicketTypes_Click(object sender, EventArgs e)
        {
            string url = "EditPages/EditTicketTypes/AddTicketTypes.aspx";
            string script = "window.open('" + url + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }

        protected void gvTicketTypes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;

            GridViewRow row = gvTicketTypes.Rows[e.NewEditIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("typeID");
            int id = int.Parse(hiddenField.Value);
            string url = $"EditPages/EditTicketTypes/UpdateTicketTypes.aspx?id={id}";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }

        // Implement refunds for customers
        private void RollbackMoneyToCustomers()
        {

        }

        protected void gvTicketTypes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // RollbackMoneyToCustomers();
            GridViewRow row = gvTicketTypes.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("typeID");
            int id = int.Parse(hiddenField.Value);
            TicketTypesBLL bLL = new TicketTypesBLL();
            bLL.DeleteTicketType(id);
            gvTicketTypes.EditIndex = -1;
            LoadData();
        }
    }
}