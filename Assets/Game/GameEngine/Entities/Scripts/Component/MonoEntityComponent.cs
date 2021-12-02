using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class MonoEntityComponent : MonoBehaviour, IEntityComponent
    {
        public IEntity Entity { get; private set; }

        void IEntityComponent.BindEntity(IEntity entity)
        {
            this.Entity = entity;
        }

        void IEntityComponent.ResetEntity()
        {
            this.Entity = null;
        }
    }
}