namespace Prototype.GameEngine
{
    public interface IEntityComponent
    {
        void BindEntity(IEntity entity);

        void UnbindEntity();
    }
}