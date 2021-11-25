using System.Collections.Generic;

namespace DynamicObjects
{
    public sealed class PropertyBus
    {
        private readonly Dictionary<object, IPropertyProvider> propertyMap;

        public PropertyBus()
        {
            this.propertyMap = new Dictionary<object, IPropertyProvider>();
        }

        public T GetProperty<T>(object key)
        {
            var provider = this.propertyMap[key];
            return (T) provider.ProvideProperty();
        }

        public bool ContainsProperty(object key)
        {
            return this.propertyMap.ContainsKey(key);
        }

        public void AddProperty(object key, IPropertyProvider provider)
        {
            this.propertyMap.Add(key, provider);
        }

        public void RemoveProperty(object key)
        {
            this.propertyMap.Remove(key);
        }
    }
}