using System;

namespace Popups
{
    public interface IPopupSupplier
    {
        IPopup LoadPopup(Type popupType);

        void UnloadPopup(IPopup popup);
    }
}