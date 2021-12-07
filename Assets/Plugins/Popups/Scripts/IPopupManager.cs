using System;

namespace Popups
{
    public interface IPopupManager
    {
        event Action<PopupName> OnPopupShown;

        event Action<PopupName> OnPopupHidden;
        
        void ShowPopup(PopupName name, object data = null);

        bool IsPopupShown(PopupName name);

        void HidePopup(PopupName name);

        void HideAllPopups();
    }
}