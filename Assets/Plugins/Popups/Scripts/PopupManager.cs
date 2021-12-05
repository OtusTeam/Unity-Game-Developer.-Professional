using System;
using System.Collections.Generic;

namespace Popups
{
    public sealed class PopupManager : IPopupManager, IPopup.Handler
    {
        public event Action<Type> OnPopupShown;
        
        public event Action<Type> OnPopupHidden;

        private readonly IPopupSupplier supplier;

        private readonly Dictionary<Type, IPopup> activePopupMap;

        private readonly List<Type> cache;

        public PopupManager(IPopupSupplier supplier)
        {
            this.supplier = supplier;
            this.activePopupMap = new Dictionary<Type, IPopup>();
            this.cache = new List<Type>();
        }

        public void ShowPopup(Type popupType, object data = null)
        {
            if (!this.IsPopupShown(popupType))
            {
                this.ShowPopupInternal(popupType, data);
            }
        }

        public bool IsPopupShown(Type popupType)
        {
            return this.activePopupMap.ContainsKey(popupType);
        }

        public void HideAllPopups()
        {
            this.cache.Clear();
            this.cache.AddRange(this.activePopupMap.Keys);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var popupType = this.cache[i];
                this.HidePopupInternal(popupType);
            }
        }

        public void HidePopup(Type popupType)
        {
            if (this.IsPopupShown(popupType))
            {
                this.HidePopupInternal(popupType);
            }
        }
        
        void IPopup.Handler.Close(Type popupType)
        {
            this.HidePopup(popupType);
        }

        private void ShowPopupInternal(Type popupType, object data)
        {
            var popup = this.supplier.LoadPopup(popupType);
            popup.Show(this, data);
            this.activePopupMap.Add(popupType, popup);
            this.OnPopupShown?.Invoke(popupType);
        }

        private void HidePopupInternal(Type popupType)
        {
            var popup = this.activePopupMap[popupType];
            popup.Hide();
            this.activePopupMap.Remove(popupType);
            this.supplier.UnloadPopup(popup);
            this.OnPopupHidden?.Invoke(popupType);
        }
    }
}