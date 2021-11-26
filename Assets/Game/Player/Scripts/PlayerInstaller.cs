using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private WeaponCurrentManager weaponCurrentManager;

        [SerializeField]
        private WeaponsPoolManager weaponsPoolManager;

        [SerializeField]
        private DamageHandler damageHandler;
        
        public override void InstallBindings()
        {
            this.Container.Bind<WeaponCurrentManager>().FromInstance(this.weaponCurrentManager);
            this.Container.Bind<WeaponsPoolManager>().FromInstance(this.weaponsPoolManager);
            
            this.Container.Bind<DamageHandler>().FromInstance(this.damageHandler);
        }
    }
}