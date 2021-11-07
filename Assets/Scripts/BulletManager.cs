using System;
using UnityEngine;

namespace Otus
{
    public sealed class BulletManager : MonoBehaviour
    {
        public event Action<Collider> OnBulletReached; 
        
        public void LaunchBullet(Vector3 position, Quaternion rotation, Vector3 direction)
        {
            
        }
    }
}