using ETMS_DatabaseHandle.BLL;
using System;

namespace ETMS_Website.Admin.EditPages.EditVenues
{
    public partial class AddVenues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void ClearAllInputs()
        {
            txtVenueName.Text = "";
            txtVenueAddress.Text = "";
            txtVenueCapacity.Text = "";
            txtVenueCity.Text = "";
            txtVenueState.Text = "";
            txtVenueZipCode.Text = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            VenuesBLL bLL = new VenuesBLL();
            try
            {
                bLL.InsertNewVenue(
                        txtVenueName.Text.Trim(),
                        txtVenueAddress.Text.Trim(),
                        int.Parse(txtVenueCapacity.Text.Trim()),
                        txtVenueCity.Text.Trim(),
                        txtVenueState.Text.Trim(),
                        txtVenueZipCode.Text.Trim()
                    );
                ClearAllInputs();
                HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Added venue successfully");
            }
            catch (Exception)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
            }
        }
    }
}