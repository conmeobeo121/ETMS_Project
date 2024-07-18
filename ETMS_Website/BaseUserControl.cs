using System;
using System.Web.UI;

namespace ETMS_Website
{
    public class BaseUserControl : UserControl, IVisibilityChangeNotifier
    {
        public event EventHandler VisibleChanged;

        public event EventHandler DataUpdated;

        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                if (base.Visible != value)
                {
                    base.Visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnVisibleChanged(EventArgs e = null)
        {
            VisibleChanged?.Invoke(this, e);
        }

        protected virtual void OnDataUpdated(EventArgs e = null)
        {
            DataUpdated?.Invoke(this, e);
        }
    }
}