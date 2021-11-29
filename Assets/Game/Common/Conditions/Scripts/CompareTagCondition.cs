using DynamicObjects;
using UnityEngine;

namespace Otus
{
    public sealed class CompareTagCondition : MonoDynamicObjectCondition
    {
        [SerializeField]
        private new string tag;
        
        public override bool IsTrue(IMonoDynamicObject dynamicObject)
        {
            return dynamicObject.GameObject.CompareTag(this.tag);
        }
    }
}