using System;

namespace Popups
{
    public interface IPopupFactory
    {
        IPopup CreatePopup(Type popupType);
    }
}