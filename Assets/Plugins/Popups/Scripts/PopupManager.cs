using System;
using System.Collections.Generic;

namespace Popups
{
    public sealed class PopupManager : IPopupManager, IPopup.Handler
    {
        public event Action<PopupName> OnPopupShown;
        
        public event Action<PopupName> OnPopupHidden;

        private readonly IPopupSupplier supplier;

        private readonly Dictionary<PopupName, IPopup> activePopupMap;

        private readonly List<PopupName> cache;

        public PopupManager(IPopupSupplier supplier)
        {
            this.supplier = supplier;
            this.activePopupMap = new Dictionary<PopupName, IPopup>();
            this.cache = new List<PopupName>();
        }

        public void ShowPopup(PopupName name, object data = null)
        {
            if (!this.IsPopupShown(name))
            {
                this.ShowPopupInternal(name, data);
            }
        }

        public bool IsPopupShown(PopupName name)
        {
            return this.activePopupMap.ContainsKey(name);
        }

        public void HidePopup(PopupName name)
        {
            if (this.IsPopupShown(name))
            {
                this.HidePopupInternal(name);
            }
        }

        public void HideAllPopups()
        {
            this.cache.Clear();
            this.cache.AddRange(this.activePopupMap.Keys);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var popupName = this.cache[i];
                this.HidePopupInternal(popupName);
            }
        }

        void IPopup.Handler.Close(IPopup popup)
        {
            if (this.TryGetName(popup, out var popupName))
            {
                this.HidePopup(popupName);
            }
        }

        private void ShowPopupInternal(PopupName name, object data)
        {
            var popup = this.supplier.LoadPopup(name);
            popup.Show(this, data);
            
            this.activePopupMap.Add(name, popup);
            this.OnPopupShown?.Invoke(name);
        }

        private void HidePopupInternal(PopupName name)
        {
            var popup = this.activePopupMap[name];
            popup.Hide();
            
            this.activePopupMap.Remove(name);
            this.supplier.UnloadPopup(popup);
            this.OnPopupHidden?.Invoke(name);
        }

        private bool TryGetName(IPopup popup, out PopupName name)
        {
            foreach (var pair in this.activePopupMap)
            {
                var otherPopup = pair.Value;
                if (ReferenceEquals(popup, otherPopup))
                {
                    name = pair.Key;
                    return true;
                }
            }

            name = default;
            return false;
        }
    }
}