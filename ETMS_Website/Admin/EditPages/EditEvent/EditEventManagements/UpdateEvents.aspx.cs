using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EditPages.EditEvent.EditEventManagements
{
    public partial class UpdateEvents : System.Web.UI.Page
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
            int id = -1;
            string idStr = Request.QueryString["id"];
            if (!int.TryParse(idStr, out id))
            {
                string exMsg = $"Invalid id: {id}";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
                return;
            }
            LoadDataToControl(id);
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

        public void LoadDataToControl(int id)
        {
            EventsBLL bLL = new EventsBLL();
            try
            {
                var data = bLL.GetEventByID(id);
                if (!(data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0))
                {
                    string exMsg = $"Invalid id: {id}";
                    HandleFunction.GoToErrorPage(Response, Context, exMsg);
                    return;
                }
                DataTable dt = data.Tables[0];
                dtEnd.SelectedDate = (DateTime)dt.Rows[0]["EventEndDate"];
                if (dtEnd.SelectedDate < DateTime.Now.Date)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Cannot be edited because it is ended.");
                    btnUpdate.Enabled = false;
                    btnUpdate.Visible = false;
                }
                hdnID.Value = dt.Rows[0]["EventID"].ToString();
                txtEventName.Text = dt.Rows[0]["EventName"].ToString();
                txtEventDescription.Text = dt.Rows[0]["EventDescription"].ToString();
                dtStart.SelectedDate = (DateTime)dt.Rows[0]["EventStartDate"];
                dtEnd.SelectedDate = (DateTime)dt.Rows[0]["EventEndDate"];
                ddlVenues.SelectedValue = dt.Rows[0]["VenueID"].ToString();
                ddlTypes.SelectedValue = dt.Rows[0]["TypeID"].ToString();
            }
            catch (Exception)
            {
                string exMsg = $"An error occurred from server.";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
            }
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
            TicketTypesBLL bLL = new TicketTypesBLL();
            DataSet data = bLL.GetTicketTypesGreaterThanTimelineWithEventID(int.Parse(hdnID.Value), endDate);
            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "The end date cannot be set to be smaller than the expiration time of the ticket sale date.");
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cvCustomValidator.IsValid)
            {
                EventsBLL bLL = new EventsBLL();
                try
                {
                    int _id = int.Parse(hdnID.Value);
                    bLL.UpdateEvent(txtEventName.Text,
                        txtEventDescription.Text,
                        dtStart.SelectedDate,
                        dtEnd.SelectedDate,
                        int.Parse(ddlVenues.SelectedValue),
                        int.Parse(ddlTypes.SelectedValue),
                        _id
                    );
                    HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Updated event successfully");
                }
                catch
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
                }
            }
        }
    }
}