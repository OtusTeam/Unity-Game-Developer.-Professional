using System;

namespace Popups
{
    public interface IPopupManager
    {
        event Action<Type> OnPopupShown;

        event Action<Type> OnPopupHidden;
        
        void ShowPopup(Type popupType, object data = null);

        void HidePopup(Type popupType);

        void HideAllPopups();

        bool IsPopupShown(Type popupType);
    }
    
    public static class PopupManagerExtensions
    {
        public static void ShowPopup<T>(this IPopupManager it, object data = null)
        {
            it.ShowPopup(typeof(T), data);
        }

        public static void HidePopup<T>(this IPopupManager it)
        {
            it.HidePopup(typeof(T));
        }

        public static bool IsPopupShown<T>(this IPopupManager it)
        {
            return it.IsPopupShown(typeof(T));
        }
    }
}