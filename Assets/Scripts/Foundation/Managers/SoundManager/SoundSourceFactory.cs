using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class SoundSourceFactory : MonoInstaller
    {
        public int PoolSize = 32;
        public SoundSource Prefab;

        public override void InstallBindings()
        {
            Container.BindFactory<AudioClip, SoundSource, SoundSource.Factory>()
                .FromMonoPoolableMemoryPool<AudioClip, SoundSource>(Initialize);
        }

        private void Initialize(MemoryPoolInitialSizeMaxSizeBinder<SoundSource > opts)
        {
            //Начальный размер пула
            //opts.WithInitialSize(PoolSize);

            //Способ создания объекта и его места в иерархии
            if (Prefab == null)
                opts.FromNewComponentOnNewGameObject().UnderTransform(transform);
            else
                opts.FromComponentInNewPrefab(Prefab).UnderTransform(transform);
        }
    }
}
