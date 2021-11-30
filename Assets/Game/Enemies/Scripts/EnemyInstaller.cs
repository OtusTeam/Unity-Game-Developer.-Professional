using DynamicObjects;
using Otus.GameEffects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoDynamicObject enemy;

        [SerializeField]
        private EntityEffectSingleManager effectManager;

        [SerializeField]
        private WeaponAttackController weapon;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.enemy);
            this.Container.Bind<IEntityEffectManager>().FromInstance(this.effectManager);
            
            // this.Container.Bind<IWeaponAttackComponent>().FromInstance(this.weapon);
        }
    }
}