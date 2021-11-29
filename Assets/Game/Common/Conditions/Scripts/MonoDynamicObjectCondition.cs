using UnityEngine;

namespace DynamicObjects
{
    public abstract class MonoDynamicObjectCondition : MonoBehaviour
    {
        public abstract bool IsTrue(IMonoDynamicObject dynamicObject);
    }
}