namespace IgorExamples
{
    public interface IInputManager
    {
        void PushHandler(IInputHandler handler, bool isTarget = false);

        void PopHandler(IInputHandler handler);
    }
}