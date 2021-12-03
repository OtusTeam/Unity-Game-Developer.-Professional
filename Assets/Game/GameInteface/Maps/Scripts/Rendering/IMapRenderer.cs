using UnityEngine;

namespace Prototype.GameInterface
{
    public interface IMapRenderer
    {
        void Render(RectTransform layerTransform);
    }
}