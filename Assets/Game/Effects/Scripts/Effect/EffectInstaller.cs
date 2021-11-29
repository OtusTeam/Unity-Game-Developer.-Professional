using UnityEngine;
using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectInstaller : MonoInstaller
    {
        private const int INITIAL_EFFECT_COUNT = 2;
        
        [SerializeField]
        private Effect effectPrefab;
        
        [SerializeField]
        private Transform effectContainer;
        
        public override void InstallBindings()
        {
            this.Container.BindMemoryPool<Effect, EffectComponentPool>()
                .WithInitialSize(INITIAL_EFFECT_COUNT)
                .FromComponentInNewPrefab(this.effectPrefab)
                .UnderTransform(this.effectContainer);
        }
    }
}