using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MonoEntity : MonoBehaviour, IEntity
    {
        private readonly Entity entity;

        public MonoEntity()
        {
            this.entity = new Entity();
        }

        void IEntity.AddComponent<T>(T component)
        {
            this.entity.AddComponent(component);
        }

        void IEntity.RemoveComponent<T>()
        {
            this.entity.RemoveComponent<T>();
        }

        IEnumerable<T> IEntity.GetComponents<T>()
        {
            return this.entity.GetComponents<T>();
        }

        void IEntity.AddTag(TagType tag)
        {
            this.entity.AddTag(tag);
        }

        void IEntity.RemoveTag(TagType tag)
        {
            this.entity.RemoveTag(tag);
        }

        bool IEntity.ContainsTag(TagType tag)
        {
            return this.entity.ContainsTag(tag);
        }

        IGameSystem IEntity.GetContext()
        {
            return this.entity.GetContext();
        }

        void IEntity.BindContext(IGameSystem gameSystem)
        {
            this.entity.BindContext(gameSystem);
        }

        void IEntity.ResetContext()
        {
            this.entity.ResetContext();
        }
    }
}