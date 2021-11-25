using UnityEngine;

namespace DynamicObjects
{
    public sealed class UnityDynamicObject : MonoBehaviour, IDynamicObject
    {
        [SerializeField]
        private GameObject[] dynamicControllers;

        private readonly DynamicObject dynamicObject;

        public UnityDynamicObject()
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

        private void Start()
        {
            this.InitializeControllers();
        }

        private void InitializeControllers()
        {
            for (int i = 0, count = this.dynamicControllers.Length; i < count; i++)
            {
                var controllerGO = this.dynamicControllers[i];
                if (controllerGO != null && controllerGO.TryGetComponent(out IDynamicObjectController controller))
                {
                    controller.Initialize(this.dynamicObject);
                }
            }
        }
    }
}