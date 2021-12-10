using System.Collections.Generic;
using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class PopupSupplier : MonoBehaviour, IPopupSupplier
    {
        [SerializeField]
        private PopupFactory factory;
        
        private Dictionary<PopupName, Popup> cashedPopupMap;

        private void Awake()
        {
            this.cashedPopupMap = new Dictionary<PopupName, Popup>();
        }

        IPopup IPopupSupplier.LoadPopup(PopupName name)
        {
            if (this.cashedPopupMap.TryGetValue(name, out var popup))
            {
                popup.gameObject.SetActive(true);
            }
            else
            {
                popup = this.factory.CreatePopup(name);
                this.cashedPopupMap.Add(name, popup);
            }

            popup.transform.SetAsLastSibling();
            return popup;
        }

        void IPopupSupplier.UnloadPopup(IPopup popup)
        {
            if (popup is Popup monoPopup)
            {
                monoPopup.gameObject.SetActive(false);
            }
        }
    }
}