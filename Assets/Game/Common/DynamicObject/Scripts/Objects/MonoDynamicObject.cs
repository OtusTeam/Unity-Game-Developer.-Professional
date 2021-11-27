using System;
using System.Collections.Generic;
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
            }
            catch (Exception)
            {
                throw new Exception($"Property of {this.name} with {key} is already added!");
            }
        }

        public void RemoveProperty(object key)
        {
            this.dynamicObject.RemoveProperty(key);
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
            }
            catch (Exception)
            {
                throw new Exception($"Method of {this.name} with {key} is already added!");
            }
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