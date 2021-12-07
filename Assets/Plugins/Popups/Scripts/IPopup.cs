namespace Popups
{
    public interface IPopup
    {
        void Show(Handler handler, IPopupArgs args = null);

        void Hide();

        public interface Handler
        {
            void Close(IPopup popup);
        }
    }
}