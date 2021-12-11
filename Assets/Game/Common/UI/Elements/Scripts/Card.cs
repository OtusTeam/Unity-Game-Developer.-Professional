using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public sealed class Card : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;

        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private Text[] parameters;
        
        public void SetTitle(string title)
        {
            this.titleText.text = title;
        }

        public void SetIcon(Sprite icon)
        {
            this.iconImage.sprite = icon;
        }

        public void SetProperty(int index, string value)
        {
            var textParameter = this.parameters[index];
            textParameter.text = value;
        }
    }
}