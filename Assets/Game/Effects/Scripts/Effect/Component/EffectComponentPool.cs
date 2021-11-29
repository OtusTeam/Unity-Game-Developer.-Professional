using Zenject;

namespace Otus.GameEffects
{
    public sealed class EffectComponentPool : MonoMemoryPool<EffectComponent>
    {
        protected override void OnCreated(EffectComponent item)
        {
            base.OnCreated(item);
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(EffectComponent item)
        {
            base.OnSpawned(item);
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(EffectComponent item)
        {
            base.OnDespawned(item);
            item.gameObject.SetActive(false);
        }
    }
}