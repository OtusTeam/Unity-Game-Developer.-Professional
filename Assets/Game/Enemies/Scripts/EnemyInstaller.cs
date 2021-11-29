using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoDynamicObject enemy;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.enemy);
        }
    }
}