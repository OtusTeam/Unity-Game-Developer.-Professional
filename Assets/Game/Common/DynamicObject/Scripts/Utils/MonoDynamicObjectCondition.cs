using UnityEngine;

namespace DynamicObjects
{
    public abstract class MonoDynamicObjectCondition : MonoBehaviour
    {
        public abstract bool IsTrue(MonoDynamicObject dynamicObject);
    }
}