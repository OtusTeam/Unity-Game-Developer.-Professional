namespace Foundation
{
    public interface ICameraManager
    {
        void ToggleFirstThirdPersonCamera(int playerIndex);

        void AddFirstPersonCamera(IFirstPersonCamera camera);

        void RemoveFirstPersonCamera(IFirstPersonCamera camera);

        void AddThirdPersonCamera(IThirdPersonCamera camera);
        
        void RemoveThirdPersonCamera(IThirdPersonCamera camera);
    }
}
