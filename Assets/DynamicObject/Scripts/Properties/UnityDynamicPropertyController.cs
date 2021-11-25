using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DynamicObjects
{
    public sealed class UnityDynamicPropertyController : MonoBehaviour, IDynamicObjectController
    {
        [SerializeField]
        private Property[] properties;

        void IDynamicObjectController.Initialize(IDynamicObject @object)
        {
            for (int i = 0, count = this.properties.Length; i < count; i++)
            {
                var property = this.properties[i];
                var key = property.key;
                var provider = new PropertyProvider(property.value);
                @object.AddProperty(key, provider);
            }
        }
        
        [Serializable]
        private struct Property
        {
            [SerializeField]
            public PropertyKey key;

            [SerializeField]
            public Object value;
        }
    }
}