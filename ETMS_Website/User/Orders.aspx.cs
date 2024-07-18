using ETMS_DatabaseHandle.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ETMS_Website.User
{
    public partial class Orders : System.Web.UI.Page
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
            CheckEmptyDataOrNot(lbEmptyOrders, rfOrders, data);
            var validTypeIDs = new HashSet<int>(data.Tables[0].AsEnumerable().Select(row => row.Field<int>("TypeID")));

            // Sử dụng LINQ để lọc cart
            var filteredCart = cart.Where(kvp => validTypeIDs.Contains(kvp.Key))
                                   .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Cập nhật lại cart với các ID hợp lệ
            int total = 0;
            cart = filteredCart;
            foreach (var d in data.Tables[0].Rows)
            {
                if (d is DataRow dr)
                {
                    total += cart[(int)dr["TypeID"]] * (int)dr["Price"];
                }
            }
            lbTotalSum.Text = $"Total payment: {total}";
            hdnTotalSum.Value = total.ToString();
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

        private void CheckTicketTypeValidAndEndSell(int typeID)
        {
            TicketTypesBLL ttBll = new TicketTypesBLL();
            DataSet data = ttBll.GetTicketTypeByID(typeID);
            if (data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "Invalid event (may be deleted, reload page to refresh)!");
                throw new Exception();
            }
            if (((DateTime)data.Tables[0].Rows[0]["EndSell"]) < DateTime.Now)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", $"Ticket sales have expired: {data.Tables[0].Rows[0]["TypeName"]}!");
                throw new Exception();
            }
        }

        private void CheckRemaingSeats(int typeID, int numSeats)
        {
            TicketTypesBLL ttBll = new TicketTypesBLL();
            DataSet data = ttBll.GetTicketTypeByID(typeID);
            EventsBLL eBLL = new EventsBLL();
            int remaningSeats = eBLL.GetRemainingSeats((int)data.Tables[0].Rows[0]["EventID"]);
            if (remaningSeats < numSeats)
            {
                HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", $"Not enough seats for the event: {data.Tables[0].Rows[0]["TypeName"]}!");
                throw new Exception();
            }
        }

        protected void cvCheckQuantityAvailable_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Dictionary<int, int> cart = Session["cartData"] as Dictionary<int, int>;
            try
            {
                if (rfOrders.Items.Count == 0)
                {
                    lbEmptyOrders.Visible = true;
                    rfOrders.Visible = false;
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", $"Empty order!");
                    throw new Exception();
                }
                foreach (var p in cart)
                {
                    CheckTicketTypeValidAndEndSell(p.Key);
                    CheckRemaingSeats(p.Key, p.Value);
                }
            }
            catch
            {
                args.IsValid = false;
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
            HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Delete a item from orders successfully.");
            LoadData();
        }

        protected void btnPaid_Click(object sender, EventArgs e)
        {
            if (cvCheckQuantityAvailable.IsValid)
            {
                int total = int.Parse(hdnTotalSum.Value);
                try
                {
                    OrdersBLL oBLL = new OrdersBLL();
                    UsersBLL uBLL = new UsersBLL();
                    TicketsBLL tBLL = new TicketsBLL();
                    Dictionary<int, int> cart = Session["cartData"] as Dictionary<int, int>;
                    int orderID = oBLL.InsertNewOrderAndReturnID((int)(uBLL.GetUserByUserName(Context.User.Identity.Name).Tables[0].Rows[0]["UserID"]),
                        DateTime.Now,
                        total,
                        "PAID");
                    foreach (var p in cart)
                    {
                        for (int i = 0; i < p.Value; ++i)
                        {
                            tBLL.InsertNewTicket(orderID, p.Key, Guid.NewGuid().ToString());
                        }
                    }
                    HandleFunction.SetupToastr(this, this.GetType(), "success", "Success", "Success order.");
                    Session["cartData"] = new Dictionary<int, int>();
                    LoadData();
                }
                catch (Exception)
                {
                    HandleFunction.SetupToastr(this, this.GetType(), "error", "Error", "An error occurred from server.");
                }
            }
        }
    }
}