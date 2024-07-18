using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace ETMS_Website.User
{
    public partial class Events : System.Web.UI.Page
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
            LoadTypeEvents();
            LoadEvents();
        }

        private void LoadTypeEvents()
        {
            EventTypesBLL evBLL = new EventTypesBLL();
            DataSet ds = evBLL.GetAllEventTypes();
            rblTypesEvent.DataSource = ds;
            rblTypesEvent.DataTextField = "TypeName";
            rblTypesEvent.DataValueField = "TypeID";
            rblTypesEvent.DataBind();
        }

        private void LoadEvents()
        {
            EventsBLL eBLL = new EventsBLL();
            DataSet data = eBLL.GetAvailableEvents();
            rfEvents.DataSource = data;
            rfEvents.DataBind();
        }

        protected void rblTypesEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonList list = (RadioButtonList)sender;
            string idType = list.SelectedValue;
            EventsBLL eBLL = new EventsBLL();
            DataSet data = eBLL.GetAvailableEventsByType(int.Parse(idType));
            rfEvents.DataSource = data;
            rfEvents.DataBind();
        }

        protected void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            string textSearch = ((TextBox)sender).Text;
            EventsBLL eBLL = new EventsBLL();
            DataSet data = eBLL.FilterByName(textSearch);
            rfEvents.DataSource = data;
            rfEvents.DataBind();
        }
    }
}