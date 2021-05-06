using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Experiments
{
    public sealed class SaveableEnemy : MonoBehaviour, IPoolable<IMemoryPool>
    {
        public sealed class Factory : PlaceholderFactory<SaveableEnemy>
        {
        }

        public IMemoryPool Pool { get; private set; }

        public void OnSpawned(IMemoryPool pool)
        {
            Pool = pool;
        }

        public void OnDespawned()
        {
        }
    }
}
