using UnityEngine;
using System.Collections.Generic;

namespace Foundation
{
    [CreateAssetMenu(menuName = "OTUS/Dialog")]
    public sealed class Dialog : ScriptableObject, ISerializationCallbackReceiver
    {
        [ReadOnly] public int NextUniqueId;
        [HideInInspector] public SimpleTransform2D EditorTransform = new SimpleTransform2D();
        [HideInInspector] public List<DialogNode> Nodes;

        public void OnBeforeSerialize()
        {
            if (Nodes != null) {
                foreach (var node in Nodes) {
                    if (node.NextIds != null)
                        node.NextIds.Clear();
                    else
                        node.NextIds = new List<int>();

                    if (node.Next != null) {
                        foreach (var next in node.Next)
                            node.NextIds.Add(next.UniqueId);
                    }
                }
            }
        }

        public void OnAfterDeserialize()
        {
            if (Nodes != null) {
                var dict = new Dictionary<int, DialogNode>();
                foreach (var node in Nodes)
                    dict[node.UniqueId] = node;

                foreach (var node in Nodes) {
                    if (node.Next != null)
                        node.Next.Clear();
                    else
                        node.Next = new List<DialogNode>();

                    if (node.NextIds != null) {
                        foreach (var nextId in node.NextIds)
                            node.Next.Add(dict[nextId]);
                    }
                }
            }
        }
    }
}
