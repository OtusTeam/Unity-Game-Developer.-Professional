using UnityEngine;

namespace Prototype.GameInterface
{
    public abstract class MapLayer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        private Vector2 pivot;
        
        private Rect rect;
        
        private void Start()
        {
            this.rect = this.rectTransform.rect;
            this.pivot = this.rectTransform.pivot;
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