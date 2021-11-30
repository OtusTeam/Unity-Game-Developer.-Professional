using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public class EntityEffectManagerAdapter : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        private IEntityEffectManager entityManager;
        
        private object ApplyEffect(object data)
        {
            var effect = (IEffect) data;
            this.entityManager.AddEffect(effect);
            return null;
        }

        private object CancelEffect(object data)
        {
            var effect = (IEffect) data;
            this.entityManager.RemoveEffect(effect);
            return null;
        }

        protected virtual IEntityEffectManager ProvideEffectManager(IDynamicObject target)
        {
            return new EntityEffectSingleManager(target);
        }

        #region Lifecycle

        private void Awake()
        {
            this.entityManager = this.ProvideEffectManager(this.entity);

            this.entity.AddMethod(ActionKey.START_EFFECT, new MethodDelegate(this.ApplyEffect));
            this.entity.AddMethod(ActionKey.STOP_EFFECT, new MethodDelegate(this.CancelEffect));
        }

        private void OnEnable()
        {
            this.entityManager.OnEffectAdded += this.OnEntityAdded;
            this.entityManager.OnEffectRemoved += this.OnEntityRemoved;
        }

        private void OnDisable()
        {
            this.entityManager.OnEffectAdded -= this.OnEntityAdded;
            this.entityManager.OnEffectRemoved -= this.OnEntityRemoved;
        }

        #endregion

        #region Callbacks
        
        private void OnEntityAdded(IEffect effect)
        {
            this.entity.InvokeEvent(ActionKey.START_EFFECT, effect);
        }
        
        private void OnEntityRemoved(IEffect effect)
        {
            this.entity.InvokeEvent(ActionKey.STOP_EFFECT, effect);
        }

        #endregion
    }
}