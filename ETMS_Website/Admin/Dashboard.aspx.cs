using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;

namespace ETMS_Website.Admin
{
    public partial class Dashboard : System.Web.UI.Page
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
            LoadEventTypeChart();
        }

        private void LoadEventTypeChart()
        {
            EventsBLL eventsBLL = new EventsBLL();
            DataSet ds = eventsBLL.GetEventTypeCounts();
            DataTable dt = ds.Tables[0];
            EventTypeChart.Series["Series1"].Points.DataBindXY(dt.Rows, "TypeName", dt.Rows, "EventCount");
            EventTypeChart.ChartAreas["ChartArea1"].AxisX.Title = "Event Types";
            EventTypeChart.ChartAreas["ChartArea1"].AxisY.Title = "Event Count";
        }
    }
}