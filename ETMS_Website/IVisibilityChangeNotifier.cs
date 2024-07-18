using System;

namespace ETMS_Website
{
    public interface IVisibilityChangeNotifier
    {
        event EventHandler VisibleChanged;
    }
}
