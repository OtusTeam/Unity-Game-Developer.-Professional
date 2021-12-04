using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public class MonoGameElementGroup : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        protected Object[] gameElements;

        public IEnumerator<IGameElement> GetEnumerator()
        {
            foreach (var element in this.gameElements)
            {
                if (element is IGameElement gameElement)
                {
                    yield return gameElement;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}