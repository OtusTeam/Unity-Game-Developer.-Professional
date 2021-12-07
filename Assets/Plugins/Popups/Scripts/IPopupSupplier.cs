namespace Popups
{
    public interface IPopupSupplier
    {
        IPopup LoadPopup(PopupName popupName);

        void UnloadPopup(IPopup popup);
    }
}