using System;
using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class PopupFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private Transform inactiveRoot;

        [SerializeField]
        private PopupAssets resources;
        
        //Тупик с прокидыванием ссылки...
        public IPopup CreatePopup(PopupName popupType)
        {
            var prefab = this.resources.LoadPrefab(popupType);
            var popup = Instantiate(prefab, this.inactiveRoot);
            popup.transform.SetParent(this.container);
            return popup;
        }
    }
}