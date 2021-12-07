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

        [SerializeField]
        private MonoInjector[] injectors;

        public Popup CreatePopup(PopupName name)
        {
            var prefab = this.resources.LoadPrefab(name);
            var popup = Instantiate(prefab, this.container);

            //DI:
            foreach (var injector in this.injectors)
            {
                injector.InjectContextInto(popup);
            }

            return popup;
        }
    }
}