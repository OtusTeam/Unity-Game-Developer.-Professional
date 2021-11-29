using DynamicObjects;
using UnityEngine;

namespace Otus
{
    [RequireComponent(typeof(Collider))]
    public sealed class MonoDynamicObjectCollider : MonoBehaviour, IMonoDynamicObject
    {
        [SerializeField]
        private MonoDynamicObject target;

        public GameObject GameObject
        {
            get { return this.target.gameObject; }
        }

        public Transform Transform
        {
            get { return this.target.transform; }
        }

        public T GetProperty<T>(object key)
        {
            return this.target.GetProperty<T>(key);
        }

        public bool ContainsProperty(object key)
        {
            return this.target.ContainsProperty(key);
        }

        public bool TryGetProperty<T>(object key, out T property)
        {
            return this.target.TryGetProperty(key, out property);
        }

        public void AddProperty(object key, IPropertyProvider provider)
        {
            this.target.AddProperty(key, provider);
        }

        public void RemoveProperty(object key)
        {
            this.target.RemoveProperty(key);
        }

        public T InvokeMethod<T>(object key, object data = null)
        {
            return this.target.InvokeMethod<T>(key, data);
        }

        public bool TryInvokeMethod<T>(object key, object data, out T result)
        {
            return this.target.TryInvokeMethod(key, data, out result);
        }

        public void InvokeMethod(object key, object data = null)
        {
            this.target.InvokeMethod(key, data);
        }

        public bool TryInvokeMethod(object key, object data = null)
        {
            return this.target.TryInvokeMethod(key, data);
        }

        public bool ContainsMethod(object key)
        {
            return this.target.ContainsMethod(key);
        }

        public void AddMethod(object key, IMethodDelegate method)
        {
            this.target.AddMethod(key, method);
        }

        public void RemoveMethod(object key)
        {
            this.target.RemoveMethod(key);
        }

        public void AddEventListener(object key, IMethodDelegate callback)
        {
            this.target.AddEventListener(key, callback);
        }

        public void RemoveEventListener(object key, IMethodDelegate callback)
        {
            this.target.RemoveEventListener(key, callback);
        }

        public void InvokeEvent(object key, object data = null)
        {
            this.target.InvokeEvent(key, data);
        }
    }
}