using System;
using System.Collections.Generic;
using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class PopupSupplier : MonoBehaviour, IPopupSupplier
    {
        [SerializeField]
        private GameObject factoryGO;

        private IPopupFactory factory;
        
        private Dictionary<Type, Popup> cashedPopupMap;

        private void Awake()
        {
            this.factory = this.factoryGO.GetComponent<IPopupFactory>();
            this.cashedPopupMap = new Dictionary<Type, Popup>();
        }

        IPopup IPopupSupplier.LoadPopup(Type popupType)
        {
            if (this.cashedPopupMap.TryGetValue(popupType, out var popup))
            {
                popup.gameObject.SetActive(true);
                
            }
            else
            {
                popup = (Popup) this.factory.CreatePopup(popupType);
                this.cashedPopupMap.Add(popupType, popup);
            }

            popup.transform.SetAsLastSibling();
            return popup;
        }

        void IPopupSupplier.UnloadPopup(IPopup popup)
        {
            var monoPopup = (Popup) popup;
            monoPopup.gameObject.SetActive(false);
        }
    }
}