using JetBrains.Annotations;

namespace Prototype.GameEngine
{
    public interface IEntityComponent
    {
        [CanBeNull]
        public IEntity Entity { get; }

        void BindEntity(IEntity entity);

        void ResetEntity();
    }
}