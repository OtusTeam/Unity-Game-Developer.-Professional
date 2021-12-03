using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MapEntity : MonoBehaviour
    {
        public int Id { get; private set; }
        
        public void SetId(int id)
        {
            this.Id = id;
        }

        public void SetPosition(Vector2 position)
        {
        }

        public void SetSize(Vector2 size)
        {
        }

        public void SetColor(Color color)
        {
        }

        public void SetIcon(Sprite icon)
        {
        }
    }
}