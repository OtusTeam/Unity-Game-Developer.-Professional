using System;
using Prototype;
using UnityEngine;

namespace Prototype.Unity
{
    public sealed class PopupManager : MonoBehaviour, IPopupManager
    {
        public event Action<PopupName> OnPopupShown
        {
            add { this.manager.OnPopupShown += value; }
            remove { this.manager.OnPopupShown -= value; }
        }

        public event Action<PopupName> OnPopupHidden
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
            this.manager = new Prototype.PopupManager(supplier);
        }

        public void ShowPopup(PopupName name, UIArguments args)
        {
            this.manager.ShowPopup(name, args);
        }

        public bool IsPopupShown(PopupName name)
        {
            return this.manager.IsPopupShown(name);
        }

        public void HidePopup(PopupName name)
        {
            this.manager.HidePopup(name);
        }

        public void HideAllPopups()
        {
            this.manager.HideAllPopups();
        }
    }
}