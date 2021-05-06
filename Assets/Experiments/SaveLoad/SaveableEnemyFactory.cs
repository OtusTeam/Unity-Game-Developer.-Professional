using Foundation;
using UnityEngine;
using Zenject;

namespace Experiments
{
    [FactoryInstaller(typeof(SaveableEnemy.Factory))]
    [ExecuteAlways]
    public sealed class SaveableEnemyFactory : MonoInstaller
    {
        public string Id;
        public int PoolSize = 8;
        public SaveableEnemy Prefab;

        void Awake()
        {
            if (Id == null)
                Id = GetType().Name;
        }

        public override void InstallBindings()
        {
            Container.BindFactoryCustomInterface<SaveableEnemy, SaveableEnemy.Factory, IRawFactory>()
                .WithId(Id)
                .FromMonoPoolableMemoryPool<SaveableEnemy>(opts => opts
                    .WithInitialSize(PoolSize)
                    .FromComponentInNewPrefab(Prefab)
                    .UnderTransform(transform));
        }
    }
}
