using UnityEngine;

namespace Otus
{
    public sealed class EntityMoveController : MonoBehaviour
    {
        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.isEnable = value; }
        }

        public bool IsLocked
        {
            get { return this.isLocked; }
            set { this.isLocked = value; }
        }

        [SerializeField]
        private bool isEnable = true;

        [SerializeField]
        private bool isLocked;

        [SerializeField]
        private float speed;

        [SerializeField]
        private Transform moveTransform;

        private readonly FloatMultiplierGroup speedMultipliers;

        public EntityMoveController()
        {
            this.speedMultipliers = new FloatMultiplierGroup();
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            if (!this.isEnable)
            {
                return;
            }

            if (this.isLocked)
            {
                return;
            }
            
            var dSpeed = this.speed * deltaTime * this.speedMultipliers.GetValue();
            this.moveTransform.position += direction * dSpeed;
        }

        public void AddSpeedMultiplier(IMultiplier<float> multiplier)
        {
            this.speedMultipliers.AddMultiplier(multiplier);
        }

        public void RemoveSpeedMultiplier(IMultiplier<float> multiplier)
        {
            this.speedMultipliers.RemoveMultiplier(multiplier);
        }
    }
}