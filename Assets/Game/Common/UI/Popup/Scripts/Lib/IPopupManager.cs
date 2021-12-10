using System;

namespace Prototype
{
    public interface IPopupManager
    {
        event Action<PopupName> OnPopupShown;

        event Action<PopupName> OnPopupHidden;
        
        void ShowPopup(PopupName name, UIArguments args);

        bool IsPopupShown(PopupName name);

        void HidePopup(PopupName name);

        void HideAllPopups();
    }
}