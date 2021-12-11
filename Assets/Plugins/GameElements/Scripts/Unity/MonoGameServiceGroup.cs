using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameServiceGroup : MonoBehaviour, IGameServiceGroup
    {
        [SerializeField]
        private MonoBehaviour[] gameServices;
        
        public IEnumerable<object> GetServices()
        {
            for (int i = 0, count = this.gameServices.Length; i < count; i++)
            {
                yield return this.gameServices[i];
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.gameServices = GameElementsEditor.ValidateServices(this.gameServices);
        }
#endif
    }
}