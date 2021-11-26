using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoDynamicObject player;

        [Header("Move")]
        [SerializeField]
        private MoveTransformController moveController;
        
        [Header("Weapon")]
        [SerializeField]
        private WeaponCurrentManager weaponCurrentManager;

        [SerializeField]
        private WeaponsPoolManager weaponsPoolManager;

        [Header("Damage")]
        [SerializeField]
        private DealDamageHandler dealDamageHandler;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.player);

            this.Container.Bind<IMoveController>().FromInstance(this.moveController);

            this.Container.Bind<WeaponCurrentManager>().FromInstance(this.weaponCurrentManager);
            this.Container.Bind<WeaponsPoolManager>().FromInstance(this.weaponsPoolManager);
            
            this.Container.Bind<DealDamageHandler>().FromInstance(this.dealDamageHandler);
        }
    }
}