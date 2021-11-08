using UnityEngine;

namespace Otus
{
    public interface IBulletManager
    {
        void LaunchBullet(
            Vector3 position,
            Quaternion rotation,
            Vector3 direction,
            IBulletListener listener = null
        );
    }
}