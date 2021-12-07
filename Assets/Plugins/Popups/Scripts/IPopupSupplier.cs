namespace Popups
{
    public interface IPopupSupplier
    {
        IPopup LoadPopup(PopupName name);

        void UnloadPopup(IPopup popup);
    }
}