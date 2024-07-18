using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EditPages.EditTicketTypes
{
    public partial class AddTicketTypes : System.Web.UI.Page
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
            LoadEvents(ddlEvents);
        }

        private void LoadEvents(DropDownList ddlEvents)
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetAllEvents();
            ddlEvents.DataSource = data.Tables[0];
            ddlEvents.DataTextField = "EventName";
            ddlEvents.DataValueField = "EventID";
            ddlEvents.DataBind();
        }

        public void ClearAllErrors()
        {
            cvCheckTicketType.IsValid = true;
        }


        private void ClearAllInputs()
        {
            ClearAllErrors();
            txtFilterEvents.Text = "";
            txtTicketName.Text = "";
            txtPrice.Text = "";
        }

        protected void txtFilterEvents_TextChanged(object sender, EventArgs e)
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.FilterByName(txtFilterEvents.Text);
            ddlEvents.DataSource = data.Tables[0];
            ddlEvents.DataTextField = "EventName";
            ddlEvents.DataValueField = "EventID";
            ddlEvents.DataBind();
        }

        private void CheckDDLEmpty(DropDownList ddlEvents)
        {
            if (ddlEvents.Items.Count == 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "You have not selected a event yet.");
                throw new Exception();
            }
        }

        private void CheckStartSellAndEndSell(DateTime dtStart, DateTime dtEnd, DropDownList ddlEvents)
        {
            if (dtStart < DateTime.Now)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Invalid start sell");
                throw new Exception();
            }
            if (dtEnd < dtStart)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "End sell must be greater than start sell.");
            }
            if (ddlEvents.Items.Count > 0)
            {
                EventsBLL bLL = new EventsBLL();
                int eventID = int.Parse(ddlEvents.SelectedValue);
                DataSet data = new DataSet();
                if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 && ((DateTime)data.Tables[0].Rows[0]["EventEndDate"]) < dtEnd)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "The ticket sales end date cannot exceed the event end date.");
                }
            }
        }

        private void CheckCanAdd(DropDownList ddlEvents)
        {
            if (ddlEvents.Items.Count > 0)
            {
                int idEvent = int.Parse(ddlEvents.SelectedValue);
                EventsBLL bLL = new EventsBLL();
                DataSet data = bLL.GetEventByID(idEvent);
                if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 && ((DateTime)data.Tables[0].Rows[0]["EventEndDate"]) < DateTime.Now)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Cannot be edited because it is ended.");
                    throw new Exception();
                }
            }
            CheckDDLEmpty(ddlEvents);
            CheckStartSellAndEndSell(dtStartSell.SelectedDate, dtEndSell.SelectedDate, ddlEvents);
        }

        protected void cvCheckTicketType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CheckCanAdd(ddlEvents);
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (cvCheckTicketType.IsValid)
            {
                try
                {
                    TicketTypesBLL bLL = new TicketTypesBLL();
                    bLL.InsertNewTicketType(txtTicketName.Text,
                        int.Parse(txtPrice.Text),
                        int.Parse(ddlEvents.SelectedValue),
                        chkbHasSeat.Checked,
                        dtStartSell.SelectedDate,
                        dtEndSell.SelectedDate);
                    ClearAllInputs();
                    LoadEvents(ddlEvents);
                    HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Added ticket type successfully");
                }
                catch (Exception)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
                }
            }
        }
    }
}