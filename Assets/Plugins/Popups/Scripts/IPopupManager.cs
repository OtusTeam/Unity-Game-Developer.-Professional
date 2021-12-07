using System;

namespace Popups
{
    public interface IPopupManager
    {
        event Action<PopupName> OnPopupShown;

        event Action<PopupName> OnPopupHidden;
        
        void ShowPopup(PopupName name, object data = null);

        void HidePopup(PopupName name);

        void HideAllPopups();

        bool IsPopupShown(PopupName name);
    }
}