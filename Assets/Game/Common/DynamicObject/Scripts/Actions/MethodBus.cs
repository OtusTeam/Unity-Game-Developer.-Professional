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

        public T CallMethod<T>(object key, object data = null)
        {
            var methodDelegate = this.delegateMap[key];
            return (T) methodDelegate.Invoke(data);
        }

        public void CallMethod(object key, object data = null)
        {
            var methodDelegate = this.delegateMap[key];
            methodDelegate.Invoke(data);
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