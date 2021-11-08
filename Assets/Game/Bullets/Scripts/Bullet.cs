using UnityEngine;

namespace Otus
{
    public sealed class Bullet : MonoBehaviour
    {
        private IHandler handler;

        private Vector3 direction;

        private float speed;

        private float lifetime;
        
        public void SetLifetime(float lifetime)
        {
            this.lifetime = lifetime;
        }
        
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
        
        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        public bool Move()
        {
            if (this.lifetime > 0)
            {
                this.transform.position += this.speed * Time.fixedDeltaTime * this.direction;
                this.lifetime -= Time.deltaTime;
                return true;
            }

            return false;
        }
        
        public void SetHandler(IHandler handler)
        {
            this.handler = handler;
        }

        private void OnTriggerEnter(Collider other)
        {
            this.handler.OnBulletCollided(this, other);
        }

        public interface IHandler
        {
            void OnBulletCollided(Bullet bullet, Collider target);
        }
    }
}