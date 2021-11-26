using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private WeaponsCurrentController weaponsCurrentController;

        [SerializeField]
        private WeaponsPool weaponsPool;

        [SerializeField]
        private DamageHandler damageHandler;
        
        public override void InstallBindings()
        {
            this.Container.Bind<WeaponsCurrentController>().FromInstance(this.weaponsCurrentController);
            this.Container.Bind<WeaponsPool>().FromInstance(this.weaponsPool);
            this.Container.Bind<DamageHandler>().FromInstance(this.damageHandler);
        }
    }
}