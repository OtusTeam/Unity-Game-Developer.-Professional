using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Pool<T> : IPool<T> where T : Object
    {
        private readonly IFactory<T> factory;

        private readonly List<T> availableItems;

        public Pool(IFactory<T> factory, int initialSize = 0)
        {
            this.factory = factory;
            this.availableItems = new List<T>();

            for (var i = 0; i < initialSize; i++)
            {
                var item = this.factory.Instantiate();
                this.availableItems.Add(item);
            }
        }

        public T Pop()
        {
            T item;
            if (this.availableItems.Count <= 0)
            {
                item = this.factory.Instantiate();
            }
            else
            {
                var nextIndex = this.availableItems.Count - 1;
                item = this.availableItems[nextIndex];
                this.availableItems.RemoveAt(nextIndex);
            }

            return item;
        }

        public void Push(T item)
        {
            this.availableItems.Add(item);
        }
    }
}