using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class WeaponInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoDynamicObject weapon;
        
        public override void InstallBindings()
        {
            this.Container.Bind<IDynamicObject>().FromInstance(this.weapon);
        }
    }
}