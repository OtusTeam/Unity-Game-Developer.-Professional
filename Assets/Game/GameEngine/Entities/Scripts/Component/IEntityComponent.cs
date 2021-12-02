namespace GameEngine
{
    public interface IEntityComponent
    {
        public IEntity CurrentEntity { get; }

        void BindEntity(IEntity entity);

        void ResetEntity();
    }
}