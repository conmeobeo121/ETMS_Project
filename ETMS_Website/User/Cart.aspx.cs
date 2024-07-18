using ETMS_DatabaseHandle.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ETMS_Website.User
{
    public partial class Cart : System.Web.UI.Page
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
            LoadItemsFromCart();
        }

        private void LoadItemsFromCart()
        {
            Dictionary<int, int> cart = Session["cartData"] as Dictionary<int, int>;
            List<int> keys = cart.Keys.ToList();
            TicketTypesBLL ttBll = new TicketTypesBLL();
            DataSet data = ttBll.GetTicketTypesWithEventNameByIDs(keys);
            CheckEmptyDataOrNot(lbEmptyCart, rfCarts, data);
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var cart = Session["cartData"] as Dictionary<int, int>;
            Button btnDelete = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnDelete.NamingContainer;
            HiddenField hdnIDType = (HiddenField)item.FindControl("hdnIDType");
            var id = int.Parse(hdnIDType.Value);
            cart.Remove(id);
            HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Delete a item from cart successfully.");
            LoadData();
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            var cart = Session["cartData"] as Dictionary<int, int>;
            TextBox txtQuantityText = ((TextBox)sender);
            int quantity = int.Parse(txtQuantityText.Text);
            if (quantity <= 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Quantity must be greater than zero.");
                return;
            }
            RepeaterItem item = (RepeaterItem)txtQuantityText.NamingContainer;
            HiddenField hdnIDType = (HiddenField)item.FindControl("hdnIDType");
            Label lbPrice = (Label)item.FindControl("lbPrice");
            TicketTypesBLL ttBll = new TicketTypesBLL();
            int id = int.Parse(hdnIDType.Value);
            DataSet data = ttBll.GetTicketTypeByID(id);
            if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                lbPrice.Text = (((int)data.Tables[0].Rows[0]["Price"]) * quantity).ToString();
                cart[id] = quantity;
            }
        }

        protected void btnProcessToPay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/Orders.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}