using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectEntityAdapter : MonoBehaviour
    {
        [Inject]
        private IDynamicObject entity;

        [Inject]
        private IEffectEntity effectManager;
        
        private object ApplyEffect(object data)
        {
            var effect = (IEffect) data;
            this.effectManager.AddEffect(effect);
            return null;
        }

        private object CancelEffect(object data)
        {
            var effect = (IEffect) data;
            this.effectManager.RemoveEffect(effect);
            return null;
        }

        #region Lifecycle

        private void Awake()
        {
            this.entity.AddMethod(ActionKey.START_EFFECT, new MethodDelegate(this.ApplyEffect));
            this.entity.AddMethod(ActionKey.STOP_EFFECT, new MethodDelegate(this.CancelEffect));
            this.entity.AddProperty(PropertyKey.ACTIVE_EFFECTS, new PropertyDelegateProvider(this.GetActiveEffects));
        }

        private object GetActiveEffects()
        {
            return this.effectManager.GetActiveEffects();
        }

        private void OnEnable()
        {
            this.effectManager.OnEffectAdded += this.OnEffectAdded;
            this.effectManager.OnEffectRemoved += this.OnEffectRemoved;
        }

        private void OnDisable()
        {
            this.effectManager.OnEffectAdded -= this.OnEffectAdded;
            this.effectManager.OnEffectRemoved -= this.OnEffectRemoved;
        }

        #endregion

        #region Callbacks
        
        private void OnEffectAdded(IEffect effect)
        {
            this.entity.InvokeEvent(ActionKey.START_EFFECT, effect);
        }
        
        private void OnEffectRemoved(IEffect effect)
        {
            this.entity.InvokeEvent(ActionKey.STOP_EFFECT, effect);
        }

        #endregion
    }
}