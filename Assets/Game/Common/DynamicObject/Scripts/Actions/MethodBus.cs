using System.Collections.Generic;

namespace DynamicObjects
{
    public sealed class MethodBus
    {
        private readonly Dictionary<object, IMethodDelegate> delegateMap;

        public MethodBus()
        {
            this.delegateMap = new Dictionary<object, IMethodDelegate>();
        }

        public T InvokeMethod<T>(object key, object data = null)
        {
            var methodDelegate = this.delegateMap[key];
            return (T) methodDelegate.Invoke(data);
        }

        public bool TryInvokeMethod<T>(object key, object data, out T result)
        {
            if (this.delegateMap.TryGetValue(key, out var methodDelegate))
            {
                result = (T) methodDelegate.Invoke(data);
                return true;
            }

            result = default;
            return false;
        }

        public void InvokeMethod(object key, object data = null)
        {
            var methodDelegate = this.delegateMap[key];
            methodDelegate.Invoke(data);
        }

        public bool TryInvokeMethod(object key, object data)
        {
            if (this.delegateMap.TryGetValue(key, out var methodDelegate))
            {
                methodDelegate.Invoke(data);
                return true;
            }

            return false;
        }

        public bool ContainsMethod(object key)
        {
            return this.delegateMap.ContainsKey(key);
        }

        public void AddMethod(object key, IMethodDelegate method)
        {
            this.delegateMap.Add(key, method);
        }

        public void RemoveMethod(object key)
        {
            this.delegateMap.Remove(key);
        }
    }
}