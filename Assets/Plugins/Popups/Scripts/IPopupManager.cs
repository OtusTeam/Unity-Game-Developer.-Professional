using System;

namespace Popups
{
    public interface IPopupManager
    {
        event Action<PopupName> OnPopupShown;

        event Action<PopupName> OnPopupHidden;
        
        void ShowPopup(PopupName name, IPopupArgs args = null);

        bool IsPopupShown(PopupName name);

        void HidePopup(PopupName name);

        void HideAllPopups();
    }
}