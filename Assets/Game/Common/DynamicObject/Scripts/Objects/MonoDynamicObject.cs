using UnityEngine;

namespace DynamicObjects
{
    public sealed class MonoDynamicObject : MonoBehaviour, IDynamicObject
    {
        private readonly DynamicObject dynamicObject;

        public MonoDynamicObject()
        {
            this.dynamicObject = new DynamicObject();
        }

        public T GetProperty<T>(object key)
        {
            return this.dynamicObject.GetProperty<T>(key);
        }

        public bool ContainsProperty(object key)
        {
            return this.dynamicObject.ContainsProperty(key);
        }

        public void AddProperty(object key, IPropertyProvider provider)
        {
            this.dynamicObject.AddProperty(key, provider);
        }

        public void RemoveProperty(object key)
        {
            this.dynamicObject.RemoveProperty(key);
        }

        public T InvokeMethod<T>(object key, object data = null)
        {
            return this.dynamicObject.InvokeMethod<T>(key, data);
        }

        public void InvokeMethod(object key, object data = null)
        {
            this.dynamicObject.InvokeMethod(key, data);
        }

        public bool ContainsMethod(object key)
        {
            return this.dynamicObject.ContainsMethod(key);
        }

        public void AddMethod(object key, IMethodDelegate method)
        {
            this.dynamicObject.AddMethod(key, method);
        }

        public void RemoveMethod(object key)
        {
            this.dynamicObject.RemoveMethod(key);
        }

        public void AddEventListener(object key, IMethodDelegate callback)
        {
            this.dynamicObject.AddEventListener(key, callback);
        }

        public void RemoveEventListener(object key, IMethodDelegate callback)
        {
            this.dynamicObject.RemoveEventListener(key, callback);
        }

        public void InvokeEvent(object key, object data = null)
        {
            this.dynamicObject.InvokeEvent(key, data);
        }
    }
}