namespace Popups
{
    public interface IPopup
    {
        void Show(Handler handler, object data = null);

        void Hide();

        public interface Handler
        {
            void Close(IPopup popup);
        }
    }
}