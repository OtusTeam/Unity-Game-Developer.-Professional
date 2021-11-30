using UnityEngine;

namespace Otus
{
    public sealed class EntityMoveController
    {
        private readonly float baseSpeed;

        private readonly Transform moveTransform;

        private readonly FloatMultiplierGroup speedMultipliers;

        public EntityMoveController(Transform moveTransform, float speed)
        {
            this.moveTransform = moveTransform;
            this.baseSpeed = speed;
            this.speedMultipliers = new FloatMultiplierGroup();
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            var dSpeed = this.baseSpeed * deltaTime * this.speedMultipliers.GetValue();
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