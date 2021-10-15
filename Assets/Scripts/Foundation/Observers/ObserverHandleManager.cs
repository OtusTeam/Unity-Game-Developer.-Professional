using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    public sealed class ObserverHandleManager
    {
        List<ObserverHandle> handles = new List<ObserverHandle>();

        public ObserverHandle Alloc()
        {
            var handle = new ObserverHandle();
            Add(handle);
            return handle;
        }

        public void Add(ObserverHandle handle)
        {
            handles.Add(handle);
        }

        public void Observe<T>(IObserverList<T> observable, T observer)
        {
            ObserverHandle handle = null;
            observable.Add(ref handle, observer);
            Add(handle);
        }

        public void Clear()
        {
            foreach (var handle in handles)
                handle.Dispose();
            handles.Clear();
        }
    }
}
