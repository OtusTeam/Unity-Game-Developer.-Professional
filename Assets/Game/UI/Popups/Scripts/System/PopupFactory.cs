using Popups;
using UnityEngine;

namespace Prototype.UI
{
    public sealed class PopupFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private PopupAssets resources;

        public Popup CreatePopup(PopupName popupType)
        {
            var prefab = this.resources.LoadPrefab(popupType);
            var popup = Instantiate(prefab, this.container);
            return popup;
        }
    }
}