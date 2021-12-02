using System.Collections.Generic;
using GameElements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MonoEntity : MonoBehaviour, IEntity
    {
        public IGameSystem CurrentGameSystem
        {
            get { return this.entity.CurrentGameSystem; }
        }

        [SerializeField]
        private GameObject[] components;
        
        private Entity entity;

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

        void IEntity.AddFlag(FlagType flag)
        {
            this.entity.AddFlag(flag);
        }

        void IEntity.RemoveFlag(FlagType flag)
        {
            this.entity.RemoveFlag(flag);
        }

        bool IEntity.ContainsFlag(FlagType flag)
        {
            return this.entity.ContainsFlag(flag);
        }

        void IEntity.BindContext(IGameSystem gameSystem)
        {
            this.entity.BindContext(gameSystem);
        }

        void IEntity.ResetContext()
        {
            this.entity.ResetContext();
        }
        
        private void Awake()
        {
            this.entity = new Entity();
            this.InitializeComponents();
        }

        private void InitializeComponents()
        {
            for (int i = 0, count = this.components.Length; i < count; i++)
            {
                var componentGO = this.components[i];
                if (componentGO != null && componentGO.TryGetComponent(out IEntityComponent component))
                {
                    this.entity.AddComponent(component);
                }
            }
        }
    }
}