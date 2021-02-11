using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    public interface ICollectible
    {
        void Collect(IInventoryStorage storage);
    }

    public abstract class AbstractCollectible<T> : AbstractInventory<T>, ICollectible
        where T : AbstractInventoryItem
    {
        public void Collect(IInventoryStorage storage)
        {
            foreach (var item in Storage.RawItems)
                storage.Add(item.item, item.count);

            storage.Clear();

            Destroy(gameObject);
        }
    }
}
