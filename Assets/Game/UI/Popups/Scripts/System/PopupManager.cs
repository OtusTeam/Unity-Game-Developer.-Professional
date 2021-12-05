using System;
using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class PopupManager : MonoBehaviour, IPopupManager
    {
        public event Action<Type> OnPopupShown
        {
            add { this.manager.OnPopupShown += value; }
            remove { this.manager.OnPopupShown -= value; }
        }

        public event Action<Type> OnPopupHidden
        {
            add { this.manager.OnPopupHidden += value; }
            remove { this.manager.OnPopupHidden -= value; }
        }

        [SerializeField]
        private GameObject supplierGO;

        private IPopupManager manager;

        private void Awake()
        {
            var supplier = this.supplierGO.GetComponent<IPopupSupplier>();
            this.manager = new Popups.PopupManager(supplier);
        }

        public void ShowPopup(Type popupType, object data = null)
        {
            this.manager.ShowPopup(popupType, data);
        }

        public void HidePopup(Type popupType)
        {
            this.manager.HidePopup(popupType);
        }

        public void HideAllPopups()
        {
            this.manager.HideAllPopups();
        }

        public bool IsPopupShown(Type popupType)
        {
            return this.manager.IsPopupShown(popupType);
        }
    }
}