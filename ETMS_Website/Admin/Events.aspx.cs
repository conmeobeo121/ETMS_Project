using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ETMS_Website.Admin
{
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LinkEventTypes_Click(linkEventTypes, null);
            }
        }

        private void DisableVisibleAllUserControls()
        {
            ucEventTypes.Visible = false;
            ucEventManagements.Visible = false;
            ucEventImgManagements.Visible = false;
        }

        private void ClearOldFontBold()
        {
            foreach (var control in ListMenus.Controls)
            {
                if (control is LinkButton link)
                {
                    link.CssClass = link.CssClass.Replace("fw-bold", "").Trim();
                }
            }
        }

        private void PreRenderOfUserControls(object sender, EventArgs e)
        {
            ClearOldFontBold();
            if (sender is LinkButton link)
            {
                link.CssClass = link.CssClass + " fw-bold";
            }
        }

        private void AddPreRenderEvents(UserControl userControl, object sender, EventArgs e)
        {
            userControl.PreRender -= (s, args) =>
            {
                PreRenderOfUserControls(sender, e);
            };
            userControl.PreRender += (s, args) =>
            {
                PreRenderOfUserControls(sender, e);
            };
        }

        private void LoadDataInUserControl(UserControl control)
        {
            if (control is ILoadData data)
            {
                data.LoadData();
            }
        }

        private void LoadAllComponentsInUserControl(UserControl control, object sender, EventArgs e)
        {
            AddPreRenderEvents(control, sender, e);
            control.Visible = true;
            LoadDataInUserControl(control);
        }

        protected void LinkEventTypes_Click(object sender, EventArgs e)
        {
            DisableVisibleAllUserControls();
            LoadAllComponentsInUserControl(ucEventTypes, sender, e);
        }

        protected void LinkEventManagements_Click(object sender, EventArgs e)
        {
            DisableVisibleAllUserControls();
            LoadAllComponentsInUserControl(ucEventManagements, sender, e);
        }

        protected void LinkEventImgManagements_Click(object sender, EventArgs e)
        {
            DisableVisibleAllUserControls();
            LoadAllComponentsInUserControl(ucEventImgManagements, sender, e);
        }
    }
}