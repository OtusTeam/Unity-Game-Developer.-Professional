using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class BulletSystemInstaller : MonoInstaller
    {
        private const int INITIAL_BULLET_COUNT = 2;

        [SerializeField]
        private BulletManager bulletManager;
        
        [SerializeField]
        private Transform bulletContainer;

        [SerializeField]
        private Bullet bulletPrefab;

        public override void InstallBindings()
        {
            this.Container.BindMemoryPool<Bullet, BulletPool>()
                .WithInitialSize(INITIAL_BULLET_COUNT)
                .FromComponentInNewPrefab(this.bulletPrefab)
                .UnderTransform(this.bulletContainer);
            
            this.Container.Bind<IBulletManager>()
                .FromInstance(this.bulletManager);
        }
    }
}