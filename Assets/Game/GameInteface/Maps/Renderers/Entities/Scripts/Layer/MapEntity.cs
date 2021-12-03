using UnityEngine;
using UnityEngine.UI;

namespace Prototype.GameInterface
{
    public sealed class MapEntity : MonoBehaviour
    {
        public int Id { get; private set; }

        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private Image image;

        public void SetId(int id)
        {
            this.Id = id;
        }

        public void SetPosition(Vector2 position)
        {
            this.rectTransform.anchoredPosition = position;
        }

        public void SetSize(Vector2 size)
        {
            this.rectTransform.sizeDelta = size;
        }

        public void SetColor(Color color)
        {
            this.image.color = color;
        }

        public void SetIcon(Sprite icon)
        {
            this.image.sprite = icon;
        }
    }
}