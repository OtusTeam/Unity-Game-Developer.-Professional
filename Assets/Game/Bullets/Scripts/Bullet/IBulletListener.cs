using UnityEngine;

namespace Otus
{
    public interface IBulletListener
    {
        void OnBulletCollided(Collider collider);
    }
}