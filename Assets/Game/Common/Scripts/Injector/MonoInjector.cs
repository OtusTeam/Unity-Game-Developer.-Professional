using UnityEngine;

namespace Prototype
{
    public abstract class MonoInjector : MonoBehaviour, IInjector
    {
        public abstract void InjectContextInto(object target);
    }
}