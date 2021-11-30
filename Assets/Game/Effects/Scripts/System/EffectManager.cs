using System.Collections.Generic;
using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectManager : MonoBehaviour
    {
        [Inject]
        private DiContainer di;

        [SerializeField]
        private Transform container;
        
        private readonly Dictionary<EffectAsset, Effect> effectMap;

        public EffectManager()
        {
            this.effectMap = new Dictionary<EffectAsset, Effect>();
        }
        
        public void ApplyEffect(EffectAsset asset, IDynamicObject target)
        {
            if (!this.effectMap.TryGetValue(asset, out var effect))
            {
                effect = this.di.InstantiatePrefabForComponent<Effect>(asset.prefab, this.container);
                this.effectMap.Add(asset, effect);
            }
            
            target.TryInvokeMethod(ActionKey.START_EFFECT, effect);
        }
    }
}