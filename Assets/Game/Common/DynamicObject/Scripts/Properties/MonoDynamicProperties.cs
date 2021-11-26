using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DynamicObjects
{
    public sealed class MonoDynamicProperties : MonoBehaviour
    {
        [SerializeField]
        private MonoDynamicObject dynamicObject;

        [SerializeField]
        private Property[] properties;

        private void Awake()
        {
            for (int i = 0, count = this.properties.Length; i < count; i++)
            {
                var property = this.properties[i];
                var key = property.key;
                var propertyProvider = new PropertyProvider(property.value);
                this.dynamicObject.AddProperty(key, propertyProvider);
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