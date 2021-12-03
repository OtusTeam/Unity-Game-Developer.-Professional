using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class MapEntityRenderComponent : MonoEntityComponent
    {
        public abstract void OnStartRender(RectTransform plane);

        public abstract void OnUpdateRender(RectTransform plane);

        public abstract void OnFinishRender(RectTransform plane);

    }
}