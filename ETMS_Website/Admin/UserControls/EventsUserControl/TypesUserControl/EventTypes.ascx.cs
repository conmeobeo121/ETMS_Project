using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EventsUserControl
{
    public partial class EventTypes : BaseUserControl, ILoadData
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            EventTypesBLL bLL = new EventTypesBLL();
            DataSet data = bLL.GetAllEventTypes();
            if (data.Tables[0].Rows.Count > 0)
            {
                gvEventTypes.DataSource = data;
                gvEventTypes.DataBind();
            }
        }

        protected void gvEventTypes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEventTypes.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("typeID");
            EventTypesBLL bLL = new EventTypesBLL();
            bLL.DeleteEventType(int.Parse(hiddenField.Value));
            gvEventTypes.EditIndex = -1;
            LoadData();
        }

        protected void gvEventTypes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEventTypes.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void gvEventTypes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEventTypes.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("typeID");
            TextBox txtEditTypeName = (TextBox)row.FindControl("txtEditTypeName");

            EventTypesBLL bLL = new EventTypesBLL();
            bLL.UpdateEventType(txtEditTypeName.Text.Trim(), int.Parse(hiddenField.Value));
            gvEventTypes.EditIndex = -1;
            LoadData();
        }

        protected void gvEventTypes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEventTypes.EditIndex = -1;
            LoadData();
        }

        public void btnAddTypeEvent_Click(object sender, EventArgs e)
        {
            string eventType = txtEventType.Text.Trim();
            EventTypesBLL bLL = new EventTypesBLL();
            bLL.InsertNewEventType(eventType);
            txtEventType.Text = "";
            LoadData();
        }
    }
}