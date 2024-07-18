using ETMS_DatabaseHandle.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin.EventsUserControl.ImagesUserControl
{
    public partial class ImageManagements : BaseUserControl, ILoadData
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            LoadImages();
        }

        private void LoadImages()
        {
            ImagesEventBLL bLL = new ImagesEventBLL();
            DataSet data = bLL.GetAllImagesEventWithName();
            if (data.Tables[0].Rows.Count > 0)
            {
                gvImages.DataSource = data;
                gvImages.DataBind();
            }
        }


        protected void gvImages_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;

            GridViewRow row = gvImages.Rows[e.NewEditIndex];
            HiddenField hdnEventID = (HiddenField)row.FindControl("hdnImageID");
            int id = int.Parse(hdnEventID.Value);

            string url = $"EditPages/EditEvent/EditImageManagements/UpdateImages.aspx?id={id}";
            string script = "window.open('" + url + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }

        protected void gvImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvImages.Rows[e.RowIndex];
            HiddenField hdnID = (HiddenField)row.FindControl("hdnImageID");
            ImagesEventBLL bLL = new ImagesEventBLL();
            bLL.DeleteImageEvent(int.Parse(hdnID.Value));
            gvImages.EditIndex = -1;
            LoadData();
        }


        protected void btnAddImg_Click(object sender, EventArgs e)
        {
            string url = $"EditPages/EditEvent/EditImageManagements/AddImages.aspx";
            string script = "window.open('" + url + "', '_blank');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenNewPage", script, true);
        }
    }
}