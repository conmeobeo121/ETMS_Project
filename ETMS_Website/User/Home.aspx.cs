using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ETMS_Website.User
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
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

        private void LoadData()
        {
            LoadTop3UpcomingEvents();
            LoadTop3OngoingEvents();
        }

        private void LoadTop3UpcomingEvents()
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetTop3UpcomingEvents();
            CheckEmptyDataOrNot(emptyUpcoming, rpUpcomingEvents, data);
        }

        private void LoadTop3OngoingEvents()
        {
            EventsBLL bLL = new EventsBLL();
            DataSet data = bLL.GetTop3OngoingEvents();
            CheckEmptyDataOrNot(emptyOngoing, rpOngoingEvents, data);
        }
    }
}