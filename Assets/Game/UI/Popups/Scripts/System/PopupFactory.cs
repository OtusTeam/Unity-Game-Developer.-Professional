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

        [SerializeField]
        private MonoInjector[] injectors;
        
        public IPopup CreatePopup(Type popupType)
        {
            var prefab = this.resources.Load(popupType);
            var popup = Instantiate(prefab, this.inactiveRoot);

            //Dependency Injection:
            this.InjectPopup(popup);
            
            popup.transform.SetParent(this.container);
            return popup;
        }

        private void InjectPopup(Popup popup)
        {
            for (int i = 0, count = this.injectors.Length; i < count; i++)
            {
                var injector = this.injectors[i];
                injector.InjectContextInto(popup);
            }
        }
    }
}