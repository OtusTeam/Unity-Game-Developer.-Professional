using UnityEngine;

namespace GameEngine
{
    public abstract class MonoComponent : MonoBehaviour, IComponent
    {
        private IEntity entity;
        
        void IComponent.BindEntity(IEntity entity)
        {
            this.entity = entity;
        }

        void IComponent.ResetEntity()
        {
            this.entity = null;
        }

        public IEntity GetEntity()
        {
            return this.entity;
        }
    }
}