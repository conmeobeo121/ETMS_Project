using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EditPages.EditTicketTypes
{
    public partial class UpdateTicketTypes : System.Web.UI.Page
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
        private void LoadEvents(DropDownList ddlEvents)
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetAllEvents();
            ddlEvents.DataSource = data.Tables[0];
            ddlEvents.DataTextField = "EventName";
            ddlEvents.DataValueField = "EventID";
            ddlEvents.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cvCheckTicketType.IsValid)
            {
                try
                {
                    TicketTypesBLL bLL = new TicketTypesBLL();
                    bLL.UpdateTicketType(txtTicketName.Text,
                        int.Parse(txtPrice.Text),
                        int.Parse(ddlEvents.SelectedValue),
                        chkbHasSeat.Checked,
                        dtStartSell.SelectedDate,
                        dtEndSell.SelectedDate,
                        int.Parse(hdnIDTicketType.Value));
                    HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Updated ticket type successfully");
                }
                catch (Exception)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
                }
            }
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
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "End sell must be greater than start sell");
                throw new Exception();
            }
            if (ddlEvents.Items.Count > 0)
            {
                EventsBLL bLL = new EventsBLL();
                int eventID = int.Parse(ddlEvents.SelectedValue);
                DataSet data = new DataSet();
                if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 && ((DateTime)data.Tables[0].Rows[0]["EventEndDate"]) < dtEnd)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "The ticket sales end date cannot exceed the event end date.");
                    throw new Exception();
                }
            }
        }
        protected void cvCheckTicketType_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CheckDDLEmpty(ddlEvents);
                CheckStartSellAndEndSell(dtStartSell.SelectedDate, dtEndSell.SelectedDate, ddlEvents);
            }
            catch
            {
                args.IsValid = false;
            }
        }

        public void LoadDataToControl(int id)
        {
            TicketTypesBLL bLL = new TicketTypesBLL();
            try
            {
                var data = bLL.GetTicketTypeByID(id);
                if (!(data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0))
                {
                    //Response.Redirect("");
                    string exMsg = $"Invalid id: {id}";
                    HandleFunction.GoToErrorPage(Response, Context, exMsg);
                    return;
                }
                DataTable dt = data.Tables[0];
                EventsBLL bLL1 = new EventsBLL();
                hdnIDTicketType.Value = dt.Rows[0]["TypeID"].ToString();
                ddlEvents.SelectedValue = dt.Rows[0]["EventID"].ToString();
                int eventID = int.Parse(ddlEvents.SelectedValue);
                DataSet data1 = bLL1.GetEventByID(eventID);
                if (data1.Tables.Count > 0 && data1.Tables[0].Rows.Count > 0 && ((DateTime)data1.Tables[0].Rows[0]["EventEndDate"]) < DateTime.Now)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Cannot be edited because it is ended.");
                    btnUpdate.Enabled = false;
                    btnUpdate.Visible = false;
                }
                txtTicketName.Text = dt.Rows[0]["TypeName"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                chkbHasSeat.Checked = (bool)dt.Rows[0]["HasSeat"];
            }
            catch (Exception)
            {
                string exMsg = $"An error occurred from server.";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
            }
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
    }
}