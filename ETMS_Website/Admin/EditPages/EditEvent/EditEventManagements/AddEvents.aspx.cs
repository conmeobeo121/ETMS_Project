using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EditPages.EditEvent.EditEventManagements
{
    public partial class AddEvents : System.Web.UI.Page
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
            LoadVenues(ddlVenues);
            LoadTypes(ddlTypes);
        }

        private void LoadVenues(DropDownList ddl)
        {
            VenuesBLL bLL = new VenuesBLL();
            DataSet data = bLL.GetAllVenues();
            ddl.DataSource = data.Tables[0];
            ddl.DataTextField = "VenueName";
            ddl.DataValueField = "VenueID";
            ddl.DataBind();
        }

        private void LoadTypes(DropDownList ddl)
        {
            EventTypesBLL bLL = new EventTypesBLL();
            DataSet data = bLL.GetAllEventTypes();
            ddl.DataSource = data.Tables[0];
            ddl.DataTextField = "TypeName";
            ddl.DataValueField = "TypeID";
            ddl.DataBind();
        }

        protected void txtFilterVenues_TextChanged(object sender, EventArgs e)
        {
            VenuesBLL bLL = new VenuesBLL();
            DataSet data = bLL.FilterByName(txtFilterVenues.Text);
            ddlVenues.DataSource = data.Tables[0];
            ddlVenues.DataTextField = "VenueName";
            ddlVenues.DataValueField = "VenueID";
            ddlVenues.DataBind();
        }

        protected void txtFilterTypes_TextChanged(object sender, EventArgs e)
        {
            EventTypesBLL bLL = new EventTypesBLL();
            DataSet data = bLL.FilterByName(txtFilterTypes.Text);
            ddlTypes.DataSource = data.Tables[0];
            ddlTypes.DataTextField = "TypeName";
            ddlTypes.DataValueField = "TypeID";
            ddlTypes.DataBind();
        }

        private void CheckDDLEmpty(DropDownList ddlVenues, DropDownList ddlEventTypes)
        {
            if (ddlVenues.Items.Count == 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "You have not selected a location yet.");
                throw new Exception();
            }
            if (ddlEventTypes.Items.Count == 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "You have not selected a event type yet.");
                throw new Exception();
            }
        }

        private void CheckChooseDate(DateTime startDate, DateTime endDate)
        {
            if (startDate < DateTime.Now)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Start date cannot be less than now.");
                throw new Exception();
            }
            if (endDate < startDate)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Start date cannot be greater than end date.");
                throw new Exception();
            }
        }

        protected void cvCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CheckChooseDate(dtStart.SelectedDate, dtEnd.SelectedDate);
                CheckDDLEmpty(ddlVenues, ddlTypes);
            }
            catch
            {
                args.IsValid = false;
            }
        }

        private void ClearAllInputs()
        {
            txtEventName.Text = null;
            txtEventDescription.Text = null;
            txtFilterVenues.Text = null;
            txtFilterTypes.Text = null;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (cvCustomValidator.IsValid)
            {
                EventsBLL bLL = new EventsBLL();
                try
                {
                    bLL.InsertNewEvent(
                            txtEventName.Text,
                            txtEventDescription.Text,
                            dtStart.SelectedDate,
                            dtEnd.SelectedDate,
                            int.Parse(ddlVenues.SelectedValue),
                            int.Parse(ddlTypes.SelectedValue)
                        );
                    ClearAllInputs();
                    HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Added event successfully.");
                }
                catch
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
                }
            }
        }
    }
}