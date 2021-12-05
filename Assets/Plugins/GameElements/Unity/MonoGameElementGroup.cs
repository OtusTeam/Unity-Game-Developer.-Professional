using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public class MonoGameElementGroup : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        protected Object[] gameElements;
        
        public IEnumerable<IGameElement> GetElements()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var element = this.gameElements[i];
                if (element is IGameElement gameElement)
                {
                    yield return gameElement;
                }
            }
        }
    }
}