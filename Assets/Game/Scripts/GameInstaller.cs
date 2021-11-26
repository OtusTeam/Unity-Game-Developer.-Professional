using Otus;
using Zenject;

namespace Game.Scripts
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.Bind<SoundManager>().FromComponentInHierarchy().AsCached();
            this.Container.Bind<IBulletManager>().FromComponentInHierarchy().AsCached();
        }
    }
}