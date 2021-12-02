using UnityEngine;

namespace Prototype.GameEngine
{
    public abstract class MapEntityRenderComponent : MonoEntityComponent
    {
        public abstract void OnActivateRender(Transform plane);

        public abstract void OnRender(Transform plane);

        public abstract void OnDeactivateRender(Transform plane);

    }
}