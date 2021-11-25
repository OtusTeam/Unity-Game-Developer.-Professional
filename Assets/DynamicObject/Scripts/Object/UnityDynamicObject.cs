using UnityEngine;

namespace DynamicObjects
{
    public sealed class UnityDynamicObject : MonoBehaviour, IDynamicObject
    {
        [SerializeField]
        private GameObject[] dynamicComponents;

        private DynamicObject dynamicObject;

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

        public T CallMethod<T>(object key, IMethodArgs args = null)
        {
            return this.dynamicObject.CallMethod<T>(key, args);
        }

        public void CallMethod(object key, IMethodArgs args = null)
        {
            this.dynamicObject.CallMethod(key, args);
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

        public void InvokeEvent(object key, IMethodArgs args)
        {
            this.dynamicObject.InvokeEvent(key, args);
        }
        
        public void SetupComponent(IDynamicObjectComponent component)
        {
            component.Setup(this.dynamicObject);
        }

        private void Start()
        {
            this.SetupComponents();
        }

        private void SetupComponents()
        {
            this.dynamicObject = new DynamicObject();
            for (int i = 0, count = this.dynamicComponents.Length; i < count; i++)
            {
                var componentGO = this.dynamicComponents[i];
                if (componentGO != null && componentGO.TryGetComponent(out IDynamicObjectComponent component))
                {
                    this.SetupComponent(component);
                }
            }
        }
    }
}