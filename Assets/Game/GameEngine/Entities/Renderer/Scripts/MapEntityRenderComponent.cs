using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class MapEntityRenderComponent : MonoEntityComponent
    {
        public abstract void OnActivate(RectTransform plane);

        public abstract void OnUpdate(RectTransform plane);

        public abstract void OnDeactivate(RectTransform plane);

    }
}