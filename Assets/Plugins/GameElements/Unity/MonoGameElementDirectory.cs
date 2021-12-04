using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementDirectory : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private Transform[] containers;
        
        public IEnumerator<IGameElement> GetEnumerator()
        {
            foreach (var container in this.containers)
            {
                foreach (Transform child in container)
                {
                    if (child.TryGetComponent(out IGameElement gameElement))
                    {
                        yield return gameElement;
                    }
                }    
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}