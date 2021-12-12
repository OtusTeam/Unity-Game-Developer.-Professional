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

        public void Move(WorldVector moveVector)
        {
            var positionComponent = this.positionComponent.Value;
            
            var previousPosition = positionComponent.GetPosition();
            var nextPosition = new WorldVector(
                previousPosition.x + this.speed * moveVector.x,
                previousPosition.z + this.speed * moveVector.z
            );

            positionComponent.SetPosition(nextPosition);
        }
    }
}