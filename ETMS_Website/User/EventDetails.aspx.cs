using ETMS_DatabaseHandle.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ETMS_Website.User
{
    public partial class EventDetails : System.Web.UI.Page
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

        private void LoadDataToControl(int id)
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
                ImagesEventBLL imgBLL = new ImagesEventBLL();
                imgBackgroundShow.ImageUrl = imgBLL.GetTop1ImageEventByEventID(id);
                DataSet dataAllImages = imgBLL.GetImagesEventByEventID(id);
                rptIndicators.DataSource = dataAllImages;
                rptIndicators.DataBind();

                rptImages.DataSource = dataAllImages;
                rptImages.DataBind();

                lbTitleEvent.Text = dt.Rows[0]["EventName"].ToString();
                lbDescription.Text = dt.Rows[0]["EventDescription"].ToString();

                VenuesBLL venuesBLL = new VenuesBLL();
                DataSet dataVenue = venuesBLL.GetVenueByID((int)dt.Rows[0]["VenueID"]);

                lbLocation.Text = "Location: " + dataVenue.Tables[0].Rows[0]["VenueName"].ToString();
                lbAddress.Text = "Address: " + dataVenue.Tables[0].Rows[0]["VenueAddress"].ToString();
                lbCapacity.Text = "Capacity: " + dataVenue.Tables[0].Rows[0]["VenueCapacity"].ToString();

                TicketTypesBLL ticketTypesBLL = new TicketTypesBLL();
                DataSet dataTicketType = ticketTypesBLL.GetTicketTypeByEventID(id);

                CheckEmptyDataOrNot(emptyTicketTypes, rpAvailableTickets, dataTicketType);

                DataSet dataEventRelated = bLL.GetTop10RelativeEvents(id);

                CheckEmptyDataOrNot(emptyRelatedEvents, rpRelatedEvents, dataEventRelated);
            }
            catch (Exception)
            {
                string exMsg = $"An error occurred from server.";
                HandleFunction.GoToErrorPage(Response, Context, exMsg);
            }
        }

        private void CheckEmptyDataOrNot(HtmlControl tag, Repeater repeater, DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                tag.Visible = true;
                repeater.Visible = false;
            }
            else
            {
                tag.Visible = false;
                repeater.Visible = true;
                repeater.DataSource = ds;
                repeater.DataBind();
            }
        }


        protected void rpAvailableTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "buy")
            {

            }
            else if (e.CommandName.ToLower() == "addtocart")
            {
                var cart = Session["cartData"] as Dictionary<int, int>;
                TextBox txtNumber = (TextBox)e.Item.FindControl("txtNumber");
                if (cart != null && txtNumber != null)
                {
                    int typeID = int.Parse(e.CommandArgument.ToString());
                    if (cart.ContainsKey(typeID))
                    {
                        cart[typeID] += int.Parse(txtNumber.Text);
                    }
                    else
                    {
                        cart[typeID] = int.Parse(txtNumber.Text);
                    }
                }
                HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Add to cart successfully.");
            }
        }

        protected void btnViewDetailRelativeEvent_Click(object sender, EventArgs e)
        {
            string relativeID = ((LinkButton)sender).CommandArgument;
            Response.Redirect("~/User/EventDetails.aspx?id=" + relativeID, false);
        }
    }
}