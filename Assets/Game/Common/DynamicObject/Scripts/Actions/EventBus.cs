using System.Collections.Generic;

namespace DynamicObjects
{
    public sealed class EventBus
    {
        private readonly Dictionary<object, List<IMethodDelegate>> listenersMap;

        private readonly List<IMethodDelegate> processingListeners;

        public EventBus()
        {
            this.listenersMap = new Dictionary<object, List<IMethodDelegate>>();
            this.processingListeners = new List<IMethodDelegate>();
        }

        public void AddEventListener(object key, IMethodDelegate listener)
        {
            if (!this.listenersMap.TryGetValue(key, out var listeners))
            {
                listeners = new List<IMethodDelegate>();
                this.listenersMap.Add(key, listeners);
            }
            
            listeners.Add(listener);
        }

        public void RemoveEventListener(object key, IMethodDelegate callback)
        {
            if (!this.listenersMap.TryGetValue(key, out var listeners))
            {
                return;
            }

            listeners.Remove(callback);
            
            if (listeners.Count <= 0)
            {
                this.listenersMap.Remove(key);
            }
        }

        public void InvokeEvent(object key, Args args)
        {
            if (!this.listenersMap.TryGetValue(key, out var listeners))
            {
                return;
            }

            this.processingListeners.Clear();
            this.processingListeners.AddRange(listeners);
            
            for (int i = 0, count = this.processingListeners.Count; i < count; i++)
            {
                var listener = this.processingListeners[i];
                listener.Invoke(args);
            }
        }
    }
}