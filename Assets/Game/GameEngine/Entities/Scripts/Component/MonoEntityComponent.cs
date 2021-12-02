using UnityEngine;

namespace GameEngine
{
    public abstract class MonoEntityComponent : MonoBehaviour, IEntityComponent
    {
        public IEntity CurrentEntity { get; private set; }

        void IEntityComponent.BindEntity(IEntity entity)
        {
            this.CurrentEntity = entity;
        }

        void IEntityComponent.ResetEntity()
        {
            this.CurrentEntity = null;
        }
    }
}