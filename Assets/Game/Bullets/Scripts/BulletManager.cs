using System;
using System.Collections.Generic;
using UnityEngine;

namespace Otus
{
    public sealed class BulletManager : MonoBehaviour, IBulletManager, Bullet.IHandler
    {
        [SerializeField]
        private Parameters parameters;

        private Dictionary<Bullet, IBulletListener> bulletListenerMap;

        private List<Bullet> activeBullets;

        private List<Bullet> processingBullets;

        public void LaunchBullet(
            Vector3 position,
            Quaternion rotation,
            Vector3 direction,
            IBulletListener listener = null
        )
        {
            var prefab = this.parameters.bulletPrefab;
            var bulletRoot = this.parameters.bulletContainer;
            var bullet = Instantiate(prefab, position, rotation, bulletRoot);
            bullet.SetHandler(this);
            bullet.SetDirection(direction);
            bullet.SetLifetime(this.parameters.config.lifetime);
            bullet.SetSpeed(this.parameters.config.speed);
            this.activeBullets.Add(bullet);
            
            if (listener != null)
            {
                this.bulletListenerMap.Add(bullet, listener);
            }
        }

        private void FixedUpdate()
        {
            this.ProcessBullets();
        }

        private void Awake()
        {
            this.activeBullets = new List<Bullet>();
            this.processingBullets = new List<Bullet>();
            this.bulletListenerMap = new Dictionary<Bullet, IBulletListener>();
        }

        void Bullet.IHandler.OnBulletCollided(Bullet bullet, Collider target)
        {
            if (this.bulletListenerMap.TryGetValue(bullet, out var listener))
            {
                this.bulletListenerMap.Remove(bullet);
                listener.OnBulletCollided(target);
            }
        }

        private void ProcessBullets()
        {
            this.processingBullets.Clear();
            this.processingBullets.AddRange(this.activeBullets);
            for (int i = 0, count = this.processingBullets.Count; i < count; i++)
            {
                var bullet = this.processingBullets[i];
                if (!bullet.Move())
                {
                    this.DestroyBullet(bullet);
                }
            }
        }

        private void DestroyBullet(Bullet bullet)
        {
            this.bulletListenerMap.Remove(bullet);
            this.activeBullets.Remove(bullet);
            bullet.gameObject.SetActive(false);
            Destroy(bullet.gameObject, 0.1f);
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public Bullet bulletPrefab;

            [SerializeField]
            public Transform bulletContainer;

            [SerializeField]
            public BulletConfig config;
        }
    }
}