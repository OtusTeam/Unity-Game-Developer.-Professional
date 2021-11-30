using Otus;
using Otus.GameEffects;
using Zenject;

namespace Game.Scripts
{
    public sealed class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.Bind<SoundManager>().FromComponentInHierarchy().AsCached();
            this.Container.Bind<IGameManager>().FromComponentInHierarchy().AsCached();
            this.Container.Bind<EffectManager>().FromComponentInHierarchy().AsCached();
        }
    }
}