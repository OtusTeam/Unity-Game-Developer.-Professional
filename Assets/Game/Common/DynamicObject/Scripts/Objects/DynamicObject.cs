namespace DynamicObjects
{
    public sealed class DynamicObject : IDynamicObject
    {
        private readonly PropertyBus properties;

        private readonly MethodBus methodBus;

        private readonly EventBus eventBus;

        public DynamicObject()
        {
            this.properties = new PropertyBus();
            this.methodBus = new MethodBus();
            this.eventBus = new EventBus();
        }

        public T GetProperty<T>(object key)
        {
            return this.properties.GetProperty<T>(key);
        }
        
        public bool ContainsProperty(object key)
        {
            return this.properties.ContainsProperty(key);
        }

        public bool TryGetProperty<T>(object key, out T property)
        {
            return this.properties.TryGetProperty(key, out property);
        }

        public void AddProperty(object key, IPropertyProvider provider)
        {
            this.properties.AddProperty(key, provider);
        }

        public void RemoveProperty(object key)
        {
            this.properties.RemoveProperty(key);
        }

        public T InvokeMethod<T>(object key, object data = null)
        {
            return this.methodBus.InvokeMethod<T>(key, data);
        }

        public bool TryInvokeMethod<T>(object key, object data, out T result)
        {
            return this.methodBus.TryInvokeMethod(key, data, out result);
        }

        public void InvokeMethod(object key, object data = null)
        {
            this.methodBus.InvokeMethod(key, data);
        }

        public bool TryInvokeMethod(object key, object data = null)
        {
            return this.methodBus.TryInvokeMethod(key, data);
        }

        public bool ContainsMethod(object key)
        {
            return this.methodBus.ContainsMethod(key);
        }

        public void AddMethod(object key, IMethodDelegate method)
        {
            this.methodBus.AddMethod(key, method);
        }

        public void RemoveMethod(object key)
        {
            this.methodBus.RemoveMethod(key);
        }

        public void AddEventListener(object key, IMethodDelegate callback)
        {
            this.eventBus.AddEventListener(key, callback);
        }

        public void RemoveEventListener(object key, IMethodDelegate callback)
        {
            this.eventBus.RemoveEventListener(key, callback);
        }

        public void InvokeEvent(object key, object data = null)
        {
            this.eventBus.InvokeEvent(key, data);
        }
    }
}