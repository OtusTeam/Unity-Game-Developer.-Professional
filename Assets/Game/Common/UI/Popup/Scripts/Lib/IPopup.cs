namespace Prototype
{
    public interface IPopup
    {
        void Show(Handler handler, UIArguments args);

        void Hide();

        public interface Handler
        {
            void Close(IPopup popup);
        }
    }
}