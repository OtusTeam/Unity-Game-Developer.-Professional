namespace Prototype
{
    public interface IPopupSupplier
    {
        IPopup LoadPopup(PopupName name);

        void UnloadPopup(IPopup popup);
    }
}