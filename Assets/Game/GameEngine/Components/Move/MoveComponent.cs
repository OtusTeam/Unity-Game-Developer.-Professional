using System;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoveComponent : EntityComponent
    {
        public float Speed
        {
            get { return this.speed; }
        }

        [SerializeField]
        private float speed;

        private readonly Lazy<PositionComponent> positionComponent;

        public MoveComponent()
        {
            this.positionComponent = this.GetEntityComponentLazy<PositionComponent>();
        }

        public void Move(WorldVector direction, float deltaTime)
        {
            var dSpeed = this.speed * deltaTime;

            var positionComponent = this.positionComponent.Value;

            var previousPosition = positionComponent.GetPosition();
            var nextPosition = new WorldVector(
                previousPosition.x + dSpeed * direction.x,
                previousPosition.z + dSpeed * direction.z
            );

            positionComponent.SetPosition(nextPosition);
        }
    }
}