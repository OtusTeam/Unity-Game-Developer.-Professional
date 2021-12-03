using UnityEngine;

namespace Prototype.GameInterface
{
    public abstract class MapLayer
    {
        protected readonly RectTransform transform;
        
        private readonly Vector2 pivot;
        
        private readonly Rect rect;

        public MapLayer(RectTransform transform)
        {
            this.transform = transform;
            this.rect = transform.rect;
            this.pivot = transform.pivot;
        }

        protected Vector2 TransformPosition(Vector2 normalizedVector)
        {
            return this.pivot + this.TransformVector(normalizedVector);
        }

        protected Vector2 TransformVector(Vector2 normalizedVector)
        {
            var screenX = this.rect.width * normalizedVector.x;
            var screenY = this.rect.height * normalizedVector.y;
            return new Vector2(screenX, screenY);
        }
    }
}