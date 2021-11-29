using Zenject;

namespace Otus
{
    public sealed class BulletPool : MonoMemoryPool<Bullet>
    {
        protected override void OnCreated(Bullet item)
        {
            base.OnCreated(item);
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(Bullet item)
        {
            base.OnSpawned(item);
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Bullet item)
        {
            base.OnDespawned(item);
            item.gameObject.SetActive(false);
        }
    }
}