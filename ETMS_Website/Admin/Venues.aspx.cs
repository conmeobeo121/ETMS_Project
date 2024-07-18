using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin
{
    public partial class Venues : System.Web.UI.Page
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
            VenuesBLL bLL = new VenuesBLL();
            DataSet data = bLL.GetAllVenues();
            if (data.Tables[0].Rows.Count > 0)
            {
                gvVenues.DataSource = data;
                gvVenues.DataBind();
            }
        }

        protected void gvVenues_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvVenues.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("venueID");
            int id = int.Parse(hiddenField.Value);
            VenuesBLL bLL = new VenuesBLL();
            bLL.DeleteVenue(id);
            gvVenues.EditIndex = -1;
            LoadData();
        }

        protected void gvVenues_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;

            GridViewRow row = gvVenues.Rows[e.NewEditIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("venueID");
            int id = int.Parse(hiddenField.Value);
            string url = $"EditPages/EditVenues/UpdateVenues.aspx?id={id}";
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }


        protected void btnAddVenue_Click(object sender, EventArgs e)
        {
            string url = "EditPages/EditVenues/AddVenues.aspx";
            string script = "window.open('" + url + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }
    }
}