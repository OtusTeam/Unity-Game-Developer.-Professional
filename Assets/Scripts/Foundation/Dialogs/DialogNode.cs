using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation
{
    [Serializable]
    public sealed class DialogNode
    {
        public int UniqueId;
        public Rect Bounds;
        public LocalizedString Text;

        [NonSerialized] public List<DialogNode> Next;
        public List<int> NextIds;

        public bool CanConnectTo(DialogNode otherNode)
        {
            if (otherNode == this)
                return false;

            foreach (var node in Next) {
                if (node == otherNode)
                    return false;
            }

            return true;
        }
    }
}
