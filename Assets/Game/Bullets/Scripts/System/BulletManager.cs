using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class BulletManager : MonoBehaviour, IBulletManager,
        Bullet.IHandler,
        IUpdateListener
    {
        [SerializeField]
        private Parameters parameters;

        [Inject]
        private BulletPool pool;

        [Inject]
        private IGameManager gameManager;

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
            var bullet = this.pool.Spawn();
            bullet.SetHandler(this);
            bullet.SetPosition(position);
            bullet.SetRotation(rotation);
            bullet.SetDirection(direction);
            bullet.SetLifetime(this.parameters.config.lifetime);
            bullet.SetSpeed(this.parameters.config.speed);

            this.activeBullets.Add(bullet);

            if (listener != null)
            {
                this.bulletListenerMap.Add(bullet, listener);
            }
        }

        #region Lifecycle

        private void Awake()
        {
            this.activeBullets = new List<Bullet>();
            this.processingBullets = new List<Bullet>();
            this.bulletListenerMap = new Dictionary<Bullet, IBulletListener>();
        }

        private void OnEnable()
        {
            this.gameManager.AddFixedUpdateListener(this);
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            this.ProcessBullets();
        }

        private void OnDisable()
        {
            this.gameManager.RemoveFixedUpdateListener(this);
        }

        #endregion

        #region Callback

        void Bullet.IHandler.OnBulletCollided(Bullet bullet, Collider target)
        {
            if (this.bulletListenerMap.TryGetValue(bullet, out var listener))
            {
                listener.OnBulletCollided(target);
                this.DestroyBullet(bullet);
            }
        }

        #endregion

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
            this.pool.Despawn(bullet);
        }

        [Serializable]
        public sealed class Parameters
        {
            [SerializeField]
            public BulletConfig config;
        }
    }
}