using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ETMS_Website.Admin.EventsUserControl
{
    public partial class EventManagements : BaseUserControl, ILoadData
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void UpdateEvents_DataUpdated(object sender, EventArgs e)
        {
            LoadData();
        }

        private void AddEvents_DataUpdated(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetAllEventsWithTypeAndVenueName();
            if (data.Tables[0].Rows.Count > 0)
            {
                gvEvents.DataSource = data;
                gvEvents.DataBind();
            }
        }

        protected void gvEvents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;

            GridViewRow row = gvEvents.Rows[e.NewEditIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("eventID");
            int id = int.Parse(hiddenField.Value);

            string url = $"EditPages/EditEvent/EditEventManagements/UpdateEvents.aspx?id={id}";
            string script = "window.open('" + url + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }


        // Implement refunds for customers
        private void RollbackMoneyToCustomers()
        {

        }

        protected void gvEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEvents.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("eventID");
            int id = int.Parse(hiddenField.Value);
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetEventByID(id);
            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                if (((DateTime)data.Tables[0].Rows[0]["EventStartDate"]) < DateTime.Now)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Cannot delete event because the event is starting or the event has been ended.");
                    return;
                }
            }
            //RollbackMoneyToCustomers();
            bLL.DeleteEvent(id);
            gvEvents.EditIndex = -1;
            LoadData();
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            string url = "EditPages/EditEvent/EditEventManagements/AddEvents.aspx";
            string script = "window.open('" + url + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }
    }
}