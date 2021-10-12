namespace IgorExamples
{
    public interface IInputHandler
    {
        bool RequestTarget { get; }
        
        void OnIdleUpdate();

        void OnTargetUpdate();
    }
}