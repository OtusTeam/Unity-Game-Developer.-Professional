namespace DynamicObjects
{
    public sealed class DynamicObject : IDynamicObject
    {
        private readonly PropertyDictionary properties;

        private readonly MethodBus methodBus;

        private readonly EventBus eventBus;

        public DynamicObject()
        {
            this.properties = new PropertyDictionary();
            this.methodBus = new MethodBus();
        }

        public T GetProperty<T>(object key)
        {
            return this.properties.GetProperty<T>(key);
        }
        
        public bool ContainsProperty(object key)
        {
            return this.properties.ContainsProperty(key);
        }

        public void AddProperty(object key, IPropertyProvider provider)
        {
            this.properties.AddProperty(key, provider);
        }

        public void RemoveProperty(object key)
        {
            this.properties.RemoveProperty(key);
        }

        public T CallMethod<T>(object key, IMethodArgs args = null)
        {
            return this.methodBus.CallMethod<T>(key, args);
        }

        public void CallMethod(object key, IMethodArgs args = null)
        {
            this.methodBus.CallMethod(key, args);
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

        public void InvokeEvent(object key, IMethodArgs args)
        {
            this.eventBus.InvokeEvent(key, args);
        }
    }
}