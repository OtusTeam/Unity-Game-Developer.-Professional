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

        [Header("Move")]
        [SerializeField]
        private EntityMoveController moveController;

        [Header("Effects")]
        [SerializeField]
        private EffectEntity effectManager;

        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.enemy);
            this.Container.Bind<EntityMoveController>().FromInstance(this.moveController);
            this.Container.Bind<IEffectEntity>().FromInstance(this.effectManager);
        }
    }
}