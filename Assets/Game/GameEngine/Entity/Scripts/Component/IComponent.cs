namespace GameEngine
{
    public interface IComponent
    {
        void BindEntity(IEntity entity);

        void ResetEntity();
        
        IEntity GetEntity();
    }
}