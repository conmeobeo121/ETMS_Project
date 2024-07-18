using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;

namespace ETMS_Website.Admin.EditPages.EditVenues
{
    public partial class UpdateVenues : System.Web.UI.Page
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


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            VenuesBLL bLL = new VenuesBLL();
            try
            {
                int _id = int.Parse(hdnIDVenue.Value);
                bLL.UpdateVenue(
                        txtVenueName.Text.Trim(),
                        txtVenueAddress.Text.Trim(),
                        int.Parse(txtVenueCapacity.Text.Trim()),
                        txtVenueCity.Text.Trim(),
                        txtVenueState.Text.Trim(),
                        txtVenueZipCode.Text.Trim(),
                        _id
                    );
                HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Updated venue successfully");
            }
            catch (Exception)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
            }
        }

        public void LoadDataToControl(int id)
        {
            VenuesBLL bLL = new VenuesBLL();
            try
            {
                var data = bLL.GetVenueByID(id);
                if (!(data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0))
                {
                    string exMsg = $"Invalid id: {id}";
                    HandleFunction.GoToErrorPage(Response, Context, exMsg);
                    return;
                }
                DataTable dt = data.Tables[0];
                hdnIDVenue.Value = dt.Rows[0]["VenueID"].ToString();
                txtVenueName.Text = dt.Rows[0]["VenueName"].ToString();
                txtVenueAddress.Text = dt.Rows[0]["VenueAddress"].ToString();
                txtVenueCapacity.Text = dt.Rows[0]["VenueCapacity"].ToString();
                txtVenueCity.Text = dt.Rows[0]["VenueCity"].ToString();
                txtVenueState.Text = dt.Rows[0]["VenueState"].ToString();
                txtVenueZipCode.Text = dt.Rows[0]["VenueZipCode"].ToString();
            }
            catch (Exception)
            {
                string exMsg = $"An error occurred from server.";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
            }
        }
    }
}