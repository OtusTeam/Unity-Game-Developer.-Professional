using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DynamicObjects
{
    public class MonoDynamicObject : SerializedMonoBehaviour, IMonoDynamicObject
    {
        private readonly DynamicObject dynamicObject;

        GameObject IMonoDynamicObject.GameObject
        {
            get { return this.gameObject; }
        }

        Transform IMonoDynamicObject.Transform
        {
            get { return this.transform; }
        }

        public MonoDynamicObject()
        {
            this.dynamicObject = new DynamicObject();
        }

        public T GetProperty<T>(object key)
        {
            try
            {
                return this.dynamicObject.GetProperty<T>(key);
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Property of {this.name} with {key} is not found!");
            }
        }

        public bool ContainsProperty(object key)
        {
            return this.dynamicObject.ContainsProperty(key);
        }

        public bool TryGetProperty<T>(object key, out T property)
        {
            return this.dynamicObject.TryGetProperty(key, out property);
        }

        public void AddProperty(object key, IPropertyProvider provider)
        {
            try
            {
                this.dynamicObject.AddProperty(key, provider);
                this.DebugPropertyAdded(key);
            }
            catch (Exception)
            {
                throw new Exception($"Property of {this.name} with {key} is already added!");
            }
        }

        public void RemoveProperty(object key)
        {
            this.dynamicObject.RemoveProperty(key);
            this.DebugPropertyRemoved(key);
        }

        public T InvokeMethod<T>(object key, object data = null)
        {
            try
            {
                return this.dynamicObject.InvokeMethod<T>(key, data);
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Method of {this.name} with {key} is not founded!");
            }
        }

        public bool TryInvokeMethod<T>(object key, object data, out T result)
        {
            return this.dynamicObject.TryInvokeMethod(key, data, out result);
        }

        public void InvokeMethod(object key, object data = null)
        {
            try
            {
                this.dynamicObject.InvokeMethod(key, data);
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Method of {this.name} with {key} is not founded!");
            }
        }

        public bool TryInvokeMethod(object key, object data = null)
        {
            return this.dynamicObject.TryInvokeMethod(key, data);
        }

        public bool ContainsMethod(object key)
        {
            return this.dynamicObject.ContainsMethod(key);
        }

        public void AddMethod(object key, IMethodDelegate method)
        {
            try
            {
                this.dynamicObject.AddMethod(key, method);
                this.DebugMethodAdded(key);
            }
            catch (Exception)
            {
                throw new Exception($"Method of {this.name} with {key} is already added!");
            }
        }

        public void RemoveMethod(object key)
        {
            this.dynamicObject.RemoveMethod(key);
            this.DebugMethodRemoved(key);
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

#if UNITY_EDITOR
        [ReadOnly]
        [ShowInInspector]
        private List<string> properties = new List<string>();
        
        [ReadOnly]
        [ShowInInspector]
        private List<string> methods = new List<string>();
        
        [Conditional("UNITY_EDITOR")]
        private void DebugPropertyAdded(object key)
        {
            this.properties.Add(key.ToString());
        }

        [Conditional("UNITY_EDITOR")]
        private void DebugPropertyRemoved(object key)
        {
            this.properties.Remove(key.ToString());
        }

        [Conditional("UNITY_EDITOR")]
        private void DebugMethodAdded(object key)
        {
            this.methods.Add(key.ToString());
        }

        [Conditional("UNITY_EDITOR")]
        private void DebugMethodRemoved(object key)
        {
            this.methods.Remove(key.ToString());
        }
#endif
    }
}