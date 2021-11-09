using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Otus
{
    public sealed class PropertyProvider : MonoBehaviour, 
        IPropertyProvider, 
        ISerializationCallbackReceiver
    {
        [SerializeField]
        private Property[] properties;

        private Dictionary<PropertyId, object> propertyMap;

        public void Add(PropertyId id, object property)
        {
            this.propertyMap.Add(id, property);
        }

        public void Remove(PropertyId id)
        {
            this.propertyMap.Remove(id);
        }
        
        public T Get<T>(PropertyId id)
        {
            return (T) this.propertyMap[id];
        }

        public bool TryGet<T>(PropertyId id, out T property)
        {
            if (this.propertyMap.TryGetValue(id, out var result))
            {
                property = (T) result;
                return true;
            }

            property = default;
            return false;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            var count = this.properties.Length;
            this.propertyMap = new Dictionary<PropertyId, object>(count);
            for (var i = 0; i < count; i++)
            {
                var property = this.properties[i];
                this.propertyMap[property.id] =  property.value;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        [Serializable]
        private sealed class Property
        {
            [SerializeField]
            public PropertyId id;

            [SerializeField]
            public Object value;
        }
    }
}