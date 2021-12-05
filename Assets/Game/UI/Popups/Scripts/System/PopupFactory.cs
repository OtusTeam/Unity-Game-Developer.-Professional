using System;
using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public class PopupFactory : MonoBehaviour, IPopupFactory
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private Transform inactiveRoot;

        [SerializeField]
        private PopupAssets resources;

        public IPopup CreatePopup(Type popupType)
        {
            var prefab = this.resources.Load(popupType);
            var popup = Instantiate(prefab, this.inactiveRoot);

            //Dependency Injection:
            this.InjectPopup(popup);
            popup.transform.SetParent(this.container);
            return popup;
        }

        protected virtual void InjectPopup(Popup popup)
        {
        }
    }
}